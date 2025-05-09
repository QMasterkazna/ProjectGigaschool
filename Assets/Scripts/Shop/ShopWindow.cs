﻿using System.Collections.Generic;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using JetBrains.Annotations;
using Skill;
using TMPro;
using Translator;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Shop
{
    public class ShopWindow : MonoBehaviour
    {
        [SerializeField] private Button _previousButton;
        [SerializeField] private Button _nextButton;

        [SerializeField] private List<GameObject> _blocks;

        [SerializeField] private List<ShopItem> _items;
        [SerializeField] private TextMeshProUGUI _cashCount;
        private Dictionary<string, ShopItem> _itemsMap;
        private int _currentBlock = 0;
        private SkillsConfig _skillsConfig;
        private OpenedSkills _openedSkils;
        private Wallet _wallet;
        private SaveSystem _saveSystem;
        private TranslatorManager _translatorManager;

        public void Initialize(SaveSystem saveSystem, SkillsConfig skillsConfig, TranslatorManager translatorManager)
        {
            _saveSystem = saveSystem;
            _skillsConfig = skillsConfig;
            _translatorManager = translatorManager;
            _openedSkils = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);
            _wallet = (Wallet)_saveSystem.GetData(SavableObjectType.Wallet);
            InitializeItemMap();
            InitializeBlockSwitching();
            ShowShopItems();
        }

        private void ShowShopItems()
        {
            foreach (var skillsConfigSkill in _skillsConfig.Skills)
            {
                var skillWithLevel = _openedSkils.GetOrCreateSkillWithLevel(skillsConfigSkill.SkillId);
                var skillDataByLevel = skillsConfigSkill.GetSkillByLevel(skillWithLevel.Level);


                if (!_itemsMap.ContainsKey(skillsConfigSkill.SkillId)) continue;

                _itemsMap[skillsConfigSkill.SkillId].Initialize(skillId => SkillUpgrade(skillId, skillDataByLevel.Cost),
                    _translatorManager.Translate(skillsConfigSkill.SkillId + "Label"),
                    "", skillDataByLevel.Cost.ToString(), _wallet.Coins >= skillDataByLevel.Cost, skillsConfigSkill.isMaxLevel(skillWithLevel.Level));
            }
        }

        private void InitializeItemMap()
        {
            _itemsMap = new();
            foreach (var shopItem in _items)
            {
                _itemsMap[shopItem.SkillId] = shopItem;
            }
        }

        private void SkillUpgrade(string skillId, int cost)
        {
            var skillWtihLevel = _openedSkils.GetOrCreateSkillWithLevel(skillId);
            skillWtihLevel.Level++;
            // _wallet.Coins -= cost;
            _wallet.SetCoins(cost, 2);
            _saveSystem.SaveData(SavableObjectType.Wallet);
            _saveSystem.SaveData(SavableObjectType.OpenedSkills);
            ShowShopItems();
        }

        private void InitializeBlockSwitching()
        {
            _previousButton.onClick.AddListener(() => ShowBlock(_currentBlock - 1));
            _nextButton.onClick.AddListener(() => ShowBlock(_currentBlock + 1));
            ShowBlock(_currentBlock);
        }

        private void ShowBlock(int index)
        {
            for (var i = 0; i < _blocks.Count; i++)
            {
                _currentBlock = (index + _blocks.Count) % _blocks.Count;
                _blocks[i].SetActive(i == _currentBlock);
            }
        }
    }
}