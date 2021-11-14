using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopManager : MonoBehaviour
{
    private Transform _panelMenu;

    // Assign these below to appropriate objects
    public Animator animator;
    public InventoryTracker healthCost;
    public InventoryTracker weaponCost;
    public Text hMaxText;
    public Text wMaxText;
    
    // Needs to be assigned when inserted to scene
    public Text dialogueText;
    public Text _nameText;

    private PlayerMovement _pm;
    private Shooting _sh;
    private Player _pl;
    private bool _isBusy = false;
    private IEnumerator _sentenceTyper = null;
    private bool _isClosing = false;

    private void Awake()
    {
        _panelMenu = transform.Find("Panel");

        _panelMenu.gameObject.SetActive(false);
    }

    public void StartShop(string npcName, PlayerMovement pm, Shooting sh, Player pl)
    {
        if (_isBusy)
        {
            return;
        }
        
        _nameText.text = npcName;

        _pm = pm;
        _sh = sh;
        _pl = pl;
        
        _pm.OccupyPlayer();
        _isBusy = true;
        DisplayShop();
    }
    
    public void DisplayShop()
    {
        GetTiers();
        animator.SetBool("isOpen", true);
        _panelMenu.gameObject.SetActive(true);
        DisplayDialogue(ShopTexts.Intro);
    }

    public void PurchaseHealth()
    {
        
        if (_pl.upgradeTier >= 5)
        {
            DisplayDialogue(ShopTexts.AtMax, ShopTexts.Continuation);
        }
        else if (_pl.inventory.GemTransaction(Upgrades.Health[_pl.upgradeTier]))
        {
            _pl.UpgradeHealth(Upgrades.HealthIncrease[_pl.upgradeTier]);
            _pl.upgradeTier++;
            GetTiers();
            DisplayDialogue(ShopTexts.Purchase, ShopTexts.Continuation);
        }
        else
        {
            DisplayDialogue(ShopTexts.Fail, ShopTexts.Continuation);
        }
    }
    
    public void PurchaseWeapon()
    {
        if (_sh.upgradeTier >= 3)
        {
            DisplayDialogue(ShopTexts.AtMax, ShopTexts.Continuation);
        }
        else if (_pl.inventory.GemTransaction(Upgrades.Weapons[_sh.upgradeTier]))
        {
            _sh.upgradeTier++;
            GetTiers();
            DisplayDialogue(ShopTexts.Purchase, ShopTexts.Continuation);
        }
        else
        {
            DisplayDialogue(ShopTexts.Fail, ShopTexts.Continuation);
        }
    }

    private void GetTiers()
    {
        if (_pl.upgradeTier >= 5)
        {
            hMaxText.gameObject.SetActive(true);
            healthCost.gameObject.SetActive(false);
        }
        else
        {
            var arr = Upgrades.Health[_pl.upgradeTier];
            healthCost.SetGems(arr[0], arr[1], arr[2], arr[3]);
        }
        
        if (_sh.upgradeTier >= 3)
        {
            wMaxText.gameObject.SetActive(true);
            weaponCost.gameObject.SetActive(false);
        }
        else
        {
            var arr = Upgrades.Weapons[_sh.upgradeTier];
            weaponCost.SetGems(arr[0], arr[1], arr[2], arr[3]);
        }
    }

    public void DisplayDialogue(string text, string follow = null)
    {
        if (_sentenceTyper != null) // If dialogue is still being typed
        {
            StopCoroutine(_sentenceTyper);
        }

        if (_isClosing)
        {
            _sentenceTyper = ClosingTyper(text);
        }
        else
        {
            _sentenceTyper = TypeSentenceCoroutine(text, follow);
        }

        StartCoroutine(_sentenceTyper);
    }

    public void CloseShop()
    {
        hMaxText.gameObject.SetActive(false);
        healthCost.gameObject.SetActive(true);
        
        wMaxText.gameObject.SetActive(false);
        weaponCost.gameObject.SetActive(true);
        
        _panelMenu.gameObject.SetActive(false);
        _isClosing = true;
        DisplayDialogue(ShopTexts.Exit);
    }
    
    IEnumerator TypeSentenceCoroutine(string sentence, string follow = null)
    {
        dialogueText.text = "";

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.015f);
        }

        if (follow != null)
        {
            yield return new WaitForSeconds(2f);
            dialogueText.text = "";
            foreach (char letter in follow)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.015f);
            }
        }
    
        _sentenceTyper = null;
    }
    
    IEnumerator ClosingTyper(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.015f);
        }
        
        yield return new WaitForSeconds(0.75f);
        _sentenceTyper = null;
        FinishShop();
    }

    private void Update()
    {
        if (_isClosing && Input.GetKeyDown("space"))
        {
            FinishShop();
        }
    }

    private void FinishShop()
    {
        _isClosing = false;
        animator.SetBool("isOpen", false);
        _isBusy = false;
        _pm.FreePlayer();
    }

    private static class ShopTexts
    {
        public const string Intro = "What would you like to upgrade?";
        public const string Purchase = "Thank you for purchasing!";
        public const string Continuation = "Anything else?";
        public const string Fail = "It seems you do not have enough gems.";
        public const string AtMax = "This is already at max level.";
        public const string Exit = "Come back again!";
    }
    
    
    
}
