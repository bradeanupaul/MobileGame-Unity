using System.Collections;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	public Animator sceneAnim;

	public int butterflyCoinPrice1 = 500;

	public int butterflyDiamondPrice1 = 30;

	public int butterflyCoinPrice2 = 750;

	public int butterflyDiamondPrice2 = 45;

	public int butterflyCoinPrice3 = 1000;

	public int butterflyDiamondPrice3 = 60;

	public int bearCoinPrice1 = 2000;

	public int bearDiamondPrice1 = 120;

	public int bearCoinPrice2 = 2500;

	public int bearDiamondPrice2 = 150;

	public int bearCoinPrice3 = 3000;

	public int bearDiamondPrice3 = 180;

	public int price1 = 20;

	public int price2 = 20;

	public int price3 = 20;

	public int price4 = 40;

	public int price5 = 40;

	public int price6 = 40;

	public int price7 = 40;

	public int price8 = 20;

	public int price9 = 40;

	public int price10 = 20;

	public int price11 = 40;

	public int price12 = 30;

	public int price13 = 30;

	public int price14 = 30;

	public int price15 = 15;

	public int price16 = 30;

	public int price17 = 30;

	public int price18 = 40;

	public int price19 = 40;

	public int price20 = 30;

	public int price21 = 30;

	public static bool butterflyCoinBuy1 = true;

	public static bool butterflyDiamondBuy1 = true;

	public static bool butterflyEquip1 = false;

	public static bool butterflyUnequip1 = false;

	public static bool butterflyCoinBuy2 = true;

	public static bool butterflyDiamondBuy2 = true;

	public static bool butterflyEquip2 = false;

	public static bool butterflyUnequip2 = false;

	public static bool butterflyCoinBuy3 = true;

	public static bool butterflyDiamondBuy3 = true;

	public static bool butterflyEquip3 = false;

	public static bool butterflyUnequip3 = false;

	public static bool bearCoinBuy1 = true;

	public static bool bearDiamondBuy1 = true;

	public static bool bearEquip1 = false;

	public static bool bearUnequip1 = false;

	public static bool bearCoinBuy2 = true;

	public static bool bearDiamondBuy2 = true;

	public static bool bearEquip2 = false;

	public static bool bearUnequip2 = false;

	public static bool bearCoinBuy3 = true;

	public static bool bearDiamondBuy3 = true;

	public static bool bearEquip3 = false;

	public static bool bearUnequip3 = false;

	public static bool buy1 = true;

	public static bool equip1 = false;

	public static bool unequip1 = false;

	public static bool buy2 = true;

	public static bool equip2 = false;

	public static bool unequip2 = false;

	public static bool buy3 = true;

	public static bool equip3 = false;

	public static bool unequip3 = false;

	public static bool buy4 = true;

	public static bool equip4 = false;

	public static bool unequip4 = false;

	public static bool buy5 = true;

	public static bool equip5 = false;

	public static bool unequip5 = false;

	public static bool buy6 = true;

	public static bool equip6 = false;

	public static bool unequip6 = false;

	public static bool buy7 = true;

	public static bool equip7 = false;

	public static bool unequip7 = false;

	public static bool buy8 = true;

	public static bool equip8 = false;

	public static bool unequip8 = false;

	public static bool buy9 = true;

	public static bool equip9 = false;

	public static bool unequip9 = false;

	public static bool buy10 = true;

	public static bool equip10 = false;

	public static bool unequip10 = false;

	public static bool buy11 = true;

	public static bool equip11 = false;

	public static bool unequip11 = false;

	public static bool buy12 = true;

	public static bool equip12 = false;

	public static bool unequip12 = false;

	public static bool buy13 = true;

	public static bool equip13 = false;

	public static bool unequip13 = false;

	public static bool buy14 = true;

	public static bool equip14 = false;

	public static bool unequip14 = false;

	public static bool buy15 = true;

	public static bool equip15 = false;

	public static bool unequip15 = false;

	public static bool buy16 = true;

	public static bool equip16 = false;

	public static bool unequip16 = false;

	public static bool buy17 = true;

	public static bool equip17 = false;

	public static bool unequip17 = false;

	public static bool buy18 = true;

	public static bool equip18 = false;

	public static bool unequip18 = false;

	public static bool buy19 = true;

	public static bool equip19 = false;

	public static bool unequip19 = false;

	public static bool buy20 = true;

	public static bool equip20 = false;

	public static bool unequip20 = false;

	public static bool buy21 = true;

	public static bool equip21 = false;

	public static bool unequip21 = false;

	public GameObject butterflyCoinBuyItem1;

	public GameObject butterflyDiamondBuyItem1;

	public GameObject butterflyEquipItem1;

	public GameObject butterflyUnequipItem1;

	public GameObject butterflyInfoItem1;

	public GameObject butterflyCoinBuyItem2;

	public GameObject butterflyDiamondBuyItem2;

	public GameObject butterflyEquipItem2;

	public GameObject butterflyUnequipItem2;

	public GameObject butterflyInfoItem2;

	public GameObject butterflyCoinBuyItem3;

	public GameObject butterflyDiamondBuyItem3;

	public GameObject butterflyEquipItem3;

	public GameObject butterflyUnequipItem3;

	public GameObject butterflyInfoItem3;

	public GameObject bearCoinBuyItem1;

	public GameObject bearDiamondBuyItem1;

	public GameObject bearEquipItem1;

	public GameObject bearUnequipItem1;

	public GameObject bearInfoItem1;

	public GameObject bearCoinBuyItem2;

	public GameObject bearDiamondBuyItem2;

	public GameObject bearEquipItem2;

	public GameObject bearUnequipItem2;

	public GameObject bearInfoItem2;

	public GameObject bearCoinBuyItem3;

	public GameObject bearDiamondBuyItem3;

	public GameObject bearEquipItem3;

	public GameObject bearUnequipItem3;

	public GameObject bearInfoItem3;

	public GameObject buyItem1;

	public GameObject equipItem1;

	public GameObject unequipItem1;

	public GameObject infoItem1;

	public GameObject buyItem2;

	public GameObject equipItem2;

	public GameObject unequipItem2;

	public GameObject infoItem2;

	public GameObject buyItem3;

	public GameObject equipItem3;

	public GameObject unequipItem3;

	public GameObject infoItem3;

	public GameObject buyItem4;

	public GameObject equipItem4;

	public GameObject unequipItem4;

	public GameObject infoItem4;

	public GameObject buyItem5;

	public GameObject equipItem5;

	public GameObject unequipItem5;

	public GameObject infoItem5;

	public GameObject buyItem6;

	public GameObject equipItem6;

	public GameObject unequipItem6;

	public GameObject infoItem6;

	public GameObject buyItem7;

	public GameObject equipItem7;

	public GameObject unequipItem7;

	public GameObject infoItem7;

	public GameObject buyItem8;

	public GameObject equipItem8;

	public GameObject unequipItem8;

	public GameObject infoItem8;

	public GameObject buyItem9;

	public GameObject equipItem9;

	public GameObject unequipItem9;

	public GameObject infoItem9;

	public GameObject buyItem10;

	public GameObject equipItem10;

	public GameObject unequipItem10;

	public GameObject infoItem10;

	public GameObject buyItem11;

	public GameObject equipItem11;

	public GameObject unequipItem11;

	public GameObject infoItem11;

	public GameObject buyItem12;

	public GameObject equipItem12;

	public GameObject unequipItem12;

	public GameObject infoItem12;

	public GameObject buyItem13;

	public GameObject equipItem13;

	public GameObject unequipItem13;

	public GameObject infoItem13;

	public GameObject buyItem14;

	public GameObject equipItem14;

	public GameObject unequipItem14;

	public GameObject infoItem14;

	public GameObject buyItem15;

	public GameObject equipItem15;

	public GameObject unequipItem15;

	public GameObject infoItem15;

	public GameObject buyItem16;

	public GameObject equipItem16;

	public GameObject unequipItem16;

	public GameObject infoItem16;

	public GameObject buyItem17;

	public GameObject equipItem17;

	public GameObject unequipItem17;

	public GameObject infoItem17;

	public GameObject buyItem18;

	public GameObject equipItem18;

	public GameObject unequipItem18;

	public GameObject infoItem18;

	public GameObject buyItem19;

	public GameObject equipItem19;

	public GameObject unequipItem19;

	public GameObject infoItem19;

	public GameObject buyItem20;

	public GameObject equipItem20;

	public GameObject unequipItem20;

	public GameObject infoItem20;

	public GameObject buyItem21;

	public GameObject equipItem21;

	public GameObject unequipItem21;

	public GameObject infoItem21;

	public GameObject backMenu;

	public GameObject bothMenu;

	public GameObject bothMenu2;

	public GameObject bothMenu3;

	public GameObject bothMenu4;

	public GameObject bothMenu5;

	public GameObject nextMenu;

	public GameObject sector1;

	public GameObject sector2;

	public GameObject sector3;

	public GameObject sector4;

	public GameObject sector5;

	public GameObject sector6;

	public GameObject sector7;

	private void Start()
	{
		sector1.SetActive(value: true);
		sector2.SetActive(value: false);
		sector3.SetActive(value: false);
		sector4.SetActive(value: false);
		sector5.SetActive(value: false);
		sector6.SetActive(value: false);
		sector7.SetActive(value: false);
		backMenu.SetActive(value: false);
		bothMenu.SetActive(value: false);
		bothMenu2.SetActive(value: false);
		bothMenu3.SetActive(value: false);
		bothMenu4.SetActive(value: false);
		bothMenu5.SetActive(value: false);
		nextMenu.SetActive(value: true);
		if (!buy1)
		{
			buyItem1.SetActive(value: false);
		}
		if (equip1)
		{
			equipItem1.SetActive(value: true);
		}
		else
		{
			equipItem1.SetActive(value: false);
		}
		if (unequip1)
		{
			unequipItem1.SetActive(value: true);
		}
		else
		{
			unequipItem1.SetActive(value: false);
		}
		if (!buy2)
		{
			buyItem2.SetActive(value: false);
		}
		if (equip2)
		{
			equipItem2.SetActive(value: true);
		}
		else
		{
			equipItem2.SetActive(value: false);
		}
		if (unequip2)
		{
			unequipItem2.SetActive(value: true);
		}
		else
		{
			unequipItem2.SetActive(value: false);
		}
		if (!buy3)
		{
			buyItem3.SetActive(value: false);
		}
		if (equip3)
		{
			equipItem3.SetActive(value: true);
		}
		else
		{
			equipItem3.SetActive(value: false);
		}
		if (unequip3)
		{
			unequipItem3.SetActive(value: true);
		}
		else
		{
			unequipItem3.SetActive(value: false);
		}
		if (!buy4)
		{
			buyItem4.SetActive(value: false);
		}
		if (equip4)
		{
			equipItem4.SetActive(value: true);
		}
		else
		{
			equipItem4.SetActive(value: false);
		}
		if (unequip4)
		{
			unequipItem4.SetActive(value: true);
		}
		else
		{
			unequipItem4.SetActive(value: false);
		}
		if (!buy5)
		{
			buyItem5.SetActive(value: false);
		}
		if (equip5)
		{
			equipItem5.SetActive(value: true);
		}
		else
		{
			equipItem5.SetActive(value: false);
		}
		if (unequip5)
		{
			unequipItem5.SetActive(value: true);
		}
		else
		{
			unequipItem5.SetActive(value: false);
		}
		if (!buy6)
		{
			buyItem6.SetActive(value: false);
		}
		if (equip6)
		{
			equipItem6.SetActive(value: true);
		}
		else
		{
			equipItem6.SetActive(value: false);
		}
		if (unequip6)
		{
			unequipItem6.SetActive(value: true);
		}
		else
		{
			unequipItem6.SetActive(value: false);
		}
		if (!buy7)
		{
			buyItem7.SetActive(value: false);
		}
		if (equip7)
		{
			equipItem7.SetActive(value: true);
		}
		else
		{
			equipItem7.SetActive(value: false);
		}
		if (unequip7)
		{
			unequipItem7.SetActive(value: true);
		}
		else
		{
			unequipItem7.SetActive(value: false);
		}
		if (!buy8)
		{
			buyItem8.SetActive(value: false);
		}
		if (equip8)
		{
			equipItem8.SetActive(value: true);
		}
		else
		{
			equipItem8.SetActive(value: false);
		}
		if (unequip8)
		{
			unequipItem8.SetActive(value: true);
		}
		else
		{
			unequipItem8.SetActive(value: false);
		}
		if (!buy9)
		{
			buyItem9.SetActive(value: false);
		}
		if (equip9)
		{
			equipItem9.SetActive(value: true);
		}
		else
		{
			equipItem9.SetActive(value: false);
		}
		if (unequip9)
		{
			unequipItem9.SetActive(value: true);
		}
		else
		{
			unequipItem9.SetActive(value: false);
		}
		if (!buy10)
		{
			buyItem10.SetActive(value: false);
		}
		if (equip10)
		{
			equipItem10.SetActive(value: true);
		}
		else
		{
			equipItem10.SetActive(value: false);
		}
		if (unequip10)
		{
			unequipItem10.SetActive(value: true);
		}
		else
		{
			unequipItem10.SetActive(value: false);
		}
		if (!buy11)
		{
			buyItem11.SetActive(value: false);
		}
		if (equip11)
		{
			equipItem11.SetActive(value: true);
		}
		else
		{
			equipItem11.SetActive(value: false);
		}
		if (unequip11)
		{
			unequipItem11.SetActive(value: true);
		}
		else
		{
			unequipItem11.SetActive(value: false);
		}
		if (!buy12)
		{
			buyItem12.SetActive(value: false);
		}
		if (equip12)
		{
			equipItem12.SetActive(value: true);
		}
		else
		{
			equipItem12.SetActive(value: false);
		}
		if (unequip12)
		{
			unequipItem12.SetActive(value: true);
		}
		else
		{
			unequipItem12.SetActive(value: false);
		}
		if (!buy13)
		{
			buyItem13.SetActive(value: false);
		}
		if (equip13)
		{
			equipItem13.SetActive(value: true);
		}
		else
		{
			equipItem13.SetActive(value: false);
		}
		if (unequip13)
		{
			unequipItem13.SetActive(value: true);
		}
		else
		{
			unequipItem13.SetActive(value: false);
		}
		if (!buy14)
		{
			buyItem14.SetActive(value: false);
		}
		if (equip14)
		{
			equipItem14.SetActive(value: true);
		}
		else
		{
			equipItem14.SetActive(value: false);
		}
		if (unequip14)
		{
			unequipItem14.SetActive(value: true);
		}
		else
		{
			unequipItem14.SetActive(value: false);
		}
		if (!buy15)
		{
			buyItem15.SetActive(value: false);
		}
		if (equip15)
		{
			equipItem15.SetActive(value: true);
		}
		else
		{
			equipItem15.SetActive(value: false);
		}
		if (unequip15)
		{
			unequipItem15.SetActive(value: true);
		}
		else
		{
			unequipItem15.SetActive(value: false);
		}
		if (!buy16)
		{
			buyItem16.SetActive(value: false);
		}
		if (equip16)
		{
			equipItem16.SetActive(value: true);
		}
		else
		{
			equipItem16.SetActive(value: false);
		}
		if (unequip16)
		{
			unequipItem16.SetActive(value: true);
		}
		else
		{
			unequipItem16.SetActive(value: false);
		}
		if (!buy17)
		{
			buyItem17.SetActive(value: false);
		}
		if (equip17)
		{
			equipItem17.SetActive(value: true);
		}
		else
		{
			equipItem17.SetActive(value: false);
		}
		if (unequip17)
		{
			unequipItem17.SetActive(value: true);
		}
		else
		{
			unequipItem17.SetActive(value: false);
		}
		if (!buy18)
		{
			buyItem18.SetActive(value: false);
		}
		if (equip18)
		{
			equipItem18.SetActive(value: true);
		}
		else
		{
			equipItem18.SetActive(value: false);
		}
		if (unequip18)
		{
			unequipItem18.SetActive(value: true);
		}
		else
		{
			unequipItem18.SetActive(value: false);
		}
		if (!buy19)
		{
			buyItem19.SetActive(value: false);
		}
		if (equip19)
		{
			equipItem19.SetActive(value: true);
		}
		else
		{
			equipItem19.SetActive(value: false);
		}
		if (unequip19)
		{
			unequipItem19.SetActive(value: true);
		}
		else
		{
			unequipItem19.SetActive(value: false);
		}
		if (!buy20)
		{
			buyItem20.SetActive(value: false);
		}
		if (equip20)
		{
			equipItem20.SetActive(value: true);
		}
		else
		{
			equipItem20.SetActive(value: false);
		}
		if (unequip20)
		{
			unequipItem20.SetActive(value: true);
		}
		else
		{
			unequipItem20.SetActive(value: false);
		}
		if (!buy21)
		{
			buyItem21.SetActive(value: false);
		}
		if (equip21)
		{
			equipItem21.SetActive(value: true);
		}
		else
		{
			equipItem21.SetActive(value: false);
		}
		if (unequip21)
		{
			unequipItem21.SetActive(value: true);
		}
		else
		{
			unequipItem21.SetActive(value: false);
		}
	}

	private void Update()
	{
		if (PlayerPrefs.GetInt("ButterflyBuyShop1") == 1)
		{
			butterflyCoinBuyItem1.SetActive(value: false);
			butterflyDiamondBuyItem1.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("ButterflyEquipShop1") == 1)
		{
			butterflyEquipItem1.SetActive(value: false);
			CharacterController.butterfly = true;
		}
		if (PlayerPrefs.GetInt("ButterflyEquipShop1") != 1 && PlayerPrefs.GetInt("ButterflyBuyShop1") == 1)
		{
			butterflyEquipItem1.SetActive(value: true);
			CharacterController.butterfly = false;
		}
		if (PlayerPrefs.GetInt("ButterflyUnequipShop1") == 1)
		{
			butterflyUnequipItem1.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("ButterflyUnequipShop1") != 1 && PlayerPrefs.GetInt("ButterflyBuyShop1") == 1)
		{
			butterflyUnequipItem1.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ButterflyItemShop1") == 1)
		{
			butterflyInfoItem1.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("ButterflyBuyShop2") == 1)
		{
			butterflyCoinBuyItem2.SetActive(value: false);
			butterflyDiamondBuyItem2.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("ButterflyEquipShop2") == 1)
		{
			butterflyEquipItem2.SetActive(value: false);
			CharacterController.butterfly = true;
		}
		if (PlayerPrefs.GetInt("ButterflyEquipShop2") != 1 && PlayerPrefs.GetInt("ButterflyBuyShop2") == 1)
		{
			butterflyEquipItem2.SetActive(value: true);
			CharacterController.butterfly = false;
		}
		if (PlayerPrefs.GetInt("ButterflyUnequipShop2") == 1)
		{
			butterflyUnequipItem2.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("ButterflyUnequipShop2") != 1 && PlayerPrefs.GetInt("ButterflyBuyShop2") == 1)
		{
			butterflyUnequipItem2.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ButterflyItemShop2") == 1)
		{
			butterflyInfoItem2.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("ButterflyBuyShop3") == 1)
		{
			butterflyCoinBuyItem3.SetActive(value: false);
			butterflyDiamondBuyItem3.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("ButterflyEquipShop3") == 1)
		{
			butterflyEquipItem3.SetActive(value: false);
			CharacterController.butterfly = true;
		}
		if (PlayerPrefs.GetInt("ButterflyEquipShop3") != 1 && PlayerPrefs.GetInt("ButterflyBuyShop3") == 1)
		{
			butterflyEquipItem3.SetActive(value: true);
			CharacterController.butterfly = false;
		}
		if (PlayerPrefs.GetInt("ButterflyUnequipShop3") == 1)
		{
			butterflyUnequipItem3.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("ButterflyUnequipShop3") != 1 && PlayerPrefs.GetInt("ButterflyBuyShop3") == 1)
		{
			butterflyUnequipItem3.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ButterflyItemShop3") == 1)
		{
			butterflyInfoItem3.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BearBuyShop1") == 1)
		{
			bearCoinBuyItem1.SetActive(value: false);
			bearDiamondBuyItem1.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
		{
			bearEquipItem1.SetActive(value: false);
			PlayerPrefs.SetInt("Bear", 1);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt("BearEquipShop1") != 1 && PlayerPrefs.GetInt("BearBuyShop1") == 1)
		{
			bearEquipItem1.SetActive(value: true);
			PlayerPrefs.SetInt("Bear", 2);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt("BearUnequipShop1") == 1)
		{
			bearUnequipItem1.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BearUnequipShop1") != 1 && PlayerPrefs.GetInt("BearBuyShop1") == 1)
		{
			bearUnequipItem1.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("BearItemShop1") == 1)
		{
			bearInfoItem1.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BearBuyShop2") == 1)
		{
			bearCoinBuyItem2.SetActive(value: false);
			bearDiamondBuyItem2.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
		{
			bearEquipItem2.SetActive(value: false);
			PlayerPrefs.SetInt("Bear", 1);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt("BearEquipShop2") != 1 && PlayerPrefs.GetInt("BearBuyShop2") == 1)
		{
			bearEquipItem2.SetActive(value: true);
			PlayerPrefs.SetInt("Bear", 2);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt("BearUnequipShop2") == 1)
		{
			bearUnequipItem2.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BearUnequipShop2") != 1 && PlayerPrefs.GetInt("BearBuyShop2") == 1)
		{
			bearUnequipItem2.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("BearItemShop2") == 1)
		{
			bearInfoItem2.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BearBuyShop3") == 1)
		{
			bearCoinBuyItem3.SetActive(value: false);
			bearDiamondBuyItem3.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
		{
			bearEquipItem3.SetActive(value: false);
			PlayerPrefs.SetInt("Bear", 1);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt("BearEquipShop3") != 1 && PlayerPrefs.GetInt("BearBuyShop3") == 1)
		{
			bearEquipItem3.SetActive(value: true);
			PlayerPrefs.SetInt("Bear", 2);
			PlayerPrefs.Save();
		}
		if (PlayerPrefs.GetInt("BearUnequipShop3") == 1)
		{
			bearUnequipItem3.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BearUnequipShop3") != 1 && PlayerPrefs.GetInt("BearBuyShop3") == 1)
		{
			bearUnequipItem3.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("BearItemShop3") == 1)
		{
			bearInfoItem3.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop1") == 1)
		{
			buyItem1.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			equipItem1.SetActive(value: false);
			CharacterController.sparkle1 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop1") != 1 && PlayerPrefs.GetInt("BuyShop1") == 1)
		{
			equipItem1.SetActive(value: true);
			CharacterController.sparkle1 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop1") == 1)
		{
			unequipItem1.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop1") != 1 && PlayerPrefs.GetInt("BuyShop1") == 1)
		{
			unequipItem1.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop1") == 1)
		{
			infoItem1.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop2") == 1)
		{
			buyItem2.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			equipItem2.SetActive(value: false);
			CharacterController.sparkle2 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop2") != 1 && PlayerPrefs.GetInt("BuyShop2") == 1)
		{
			equipItem2.SetActive(value: true);
			CharacterController.sparkle2 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop2") == 1)
		{
			unequipItem2.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop2") != 1 && PlayerPrefs.GetInt("BuyShop2") == 1)
		{
			unequipItem2.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop2") == 1)
		{
			infoItem2.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop3") == 1)
		{
			buyItem3.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			equipItem3.SetActive(value: false);
			CharacterController.sparkle3 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop3") != 1 && PlayerPrefs.GetInt("BuyShop3") == 1)
		{
			equipItem3.SetActive(value: true);
			CharacterController.sparkle3 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop3") == 1)
		{
			unequipItem3.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop3") != 1 && PlayerPrefs.GetInt("BuyShop3") == 1)
		{
			unequipItem3.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop3") == 1)
		{
			infoItem3.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop4") == 1)
		{
			buyItem4.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			equipItem4.SetActive(value: false);
			CharacterController.sparkle4 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop4") != 1 && PlayerPrefs.GetInt("BuyShop4") == 1)
		{
			equipItem4.SetActive(value: true);
			CharacterController.sparkle4 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop4") == 1)
		{
			unequipItem4.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop4") != 1 && PlayerPrefs.GetInt("BuyShop4") == 1)
		{
			unequipItem4.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop4") == 1)
		{
			infoItem4.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop5") == 1)
		{
			buyItem5.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			equipItem5.SetActive(value: false);
			CharacterController.sparkle5 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop5") != 1 && PlayerPrefs.GetInt("BuyShop5") == 1)
		{
			equipItem5.SetActive(value: true);
			CharacterController.sparkle5 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop5") == 1)
		{
			unequipItem5.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop5") != 1 && PlayerPrefs.GetInt("BuyShop5") == 1)
		{
			unequipItem5.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop5") == 1)
		{
			infoItem5.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop6") == 1)
		{
			buyItem6.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			equipItem6.SetActive(value: false);
			CharacterController.sparkle6 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop6") != 1 && PlayerPrefs.GetInt("BuyShop6") == 1)
		{
			equipItem6.SetActive(value: true);
			CharacterController.sparkle6 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop6") == 1)
		{
			unequipItem6.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop6") != 1 && PlayerPrefs.GetInt("BuyShop6") == 1)
		{
			unequipItem6.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop6") == 1)
		{
			infoItem6.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop7") == 1)
		{
			buyItem7.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			equipItem7.SetActive(value: false);
			CharacterController.sparkle7 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop7") != 1 && PlayerPrefs.GetInt("BuyShop7") == 1)
		{
			equipItem7.SetActive(value: true);
			CharacterController.sparkle7 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop7") == 1)
		{
			unequipItem7.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop7") != 1 && PlayerPrefs.GetInt("BuyShop7") == 1)
		{
			unequipItem7.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop7") == 1)
		{
			infoItem7.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop8") == 1)
		{
			buyItem8.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			equipItem8.SetActive(value: false);
			CharacterController.sparkle8 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop8") != 1 && PlayerPrefs.GetInt("BuyShop8") == 1)
		{
			equipItem8.SetActive(value: true);
			CharacterController.sparkle8 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop8") == 1)
		{
			unequipItem8.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop8") != 1 && PlayerPrefs.GetInt("BuyShop8") == 1)
		{
			unequipItem8.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop8") == 1)
		{
			infoItem8.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop9") == 1)
		{
			buyItem9.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			equipItem9.SetActive(value: false);
			CharacterController.sparkle9 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop9") != 1 && PlayerPrefs.GetInt("BuyShop9") == 1)
		{
			equipItem9.SetActive(value: true);
			CharacterController.sparkle9 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop9") == 1)
		{
			unequipItem9.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop9") != 1 && PlayerPrefs.GetInt("BuyShop9") == 1)
		{
			unequipItem9.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop9") == 1)
		{
			infoItem9.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop10") == 1)
		{
			buyItem10.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			equipItem10.SetActive(value: false);
			CharacterController.sparkle10 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop10") != 1 && PlayerPrefs.GetInt("BuyShop10") == 1)
		{
			equipItem10.SetActive(value: true);
			CharacterController.sparkle10 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop10") == 1)
		{
			unequipItem10.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop10") != 1 && PlayerPrefs.GetInt("BuyShop10") == 1)
		{
			unequipItem10.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop10") == 1)
		{
			infoItem10.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop11") == 1)
		{
			buyItem11.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			equipItem11.SetActive(value: false);
			CharacterController.sparkle11 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop11") != 1 && PlayerPrefs.GetInt("BuyShop11") == 1)
		{
			equipItem11.SetActive(value: true);
			CharacterController.sparkle11 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop11") == 1)
		{
			unequipItem11.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop11") != 1 && PlayerPrefs.GetInt("BuyShop11") == 1)
		{
			unequipItem11.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop11") == 1)
		{
			infoItem11.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop12") == 1)
		{
			buyItem12.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			equipItem12.SetActive(value: false);
			CharacterController.sparkle12 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop12") != 1 && PlayerPrefs.GetInt("BuyShop12") == 1)
		{
			equipItem12.SetActive(value: true);
			CharacterController.sparkle12 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop12") == 1)
		{
			unequipItem12.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop12") != 1 && PlayerPrefs.GetInt("BuyShop12") == 1)
		{
			unequipItem12.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop12") == 1)
		{
			infoItem12.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop13") == 1)
		{
			buyItem13.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			equipItem13.SetActive(value: false);
			CharacterController.sparkle13 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop13") != 1 && PlayerPrefs.GetInt("BuyShop13") == 1)
		{
			equipItem13.SetActive(value: true);
			CharacterController.sparkle13 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop13") == 1)
		{
			unequipItem13.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop13") != 1 && PlayerPrefs.GetInt("BuyShop13") == 1)
		{
			unequipItem13.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop13") == 1)
		{
			infoItem13.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop14") == 1)
		{
			buyItem14.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			equipItem14.SetActive(value: false);
			CharacterController.sparkle14 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop14") != 1 && PlayerPrefs.GetInt("BuyShop14") == 1)
		{
			equipItem14.SetActive(value: true);
			CharacterController.sparkle14 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop14") == 1)
		{
			unequipItem14.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop14") != 1 && PlayerPrefs.GetInt("BuyShop14") == 1)
		{
			unequipItem14.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop14") == 1)
		{
			infoItem14.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop15") == 1)
		{
			buyItem15.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			equipItem15.SetActive(value: false);
			CharacterController.sparkle15 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop15") != 1 && PlayerPrefs.GetInt("BuyShop15") == 1)
		{
			equipItem15.SetActive(value: true);
			CharacterController.sparkle15 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop15") == 1)
		{
			unequipItem15.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop15") != 1 && PlayerPrefs.GetInt("BuyShop15") == 1)
		{
			unequipItem15.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop15") == 1)
		{
			infoItem15.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop16") == 1)
		{
			buyItem16.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			equipItem16.SetActive(value: false);
			CharacterController.sparkle16 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop16") != 1 && PlayerPrefs.GetInt("BuyShop16") == 1)
		{
			equipItem16.SetActive(value: true);
			CharacterController.sparkle16 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop16") == 1)
		{
			unequipItem16.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop16") != 1 && PlayerPrefs.GetInt("BuyShop16") == 1)
		{
			unequipItem16.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop16") == 1)
		{
			infoItem16.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop17") == 1)
		{
			buyItem17.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			equipItem17.SetActive(value: false);
			CharacterController.sparkle17 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop17") != 1 && PlayerPrefs.GetInt("BuyShop17") == 1)
		{
			equipItem17.SetActive(value: true);
			CharacterController.sparkle17 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop17") == 1)
		{
			unequipItem17.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop17") != 1 && PlayerPrefs.GetInt("BuyShop17") == 1)
		{
			unequipItem17.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop17") == 1)
		{
			infoItem17.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop18") == 1)
		{
			buyItem18.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			equipItem18.SetActive(value: false);
			CharacterController.sparkle18 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop18") != 1 && PlayerPrefs.GetInt("BuyShop18") == 1)
		{
			equipItem18.SetActive(value: true);
			CharacterController.sparkle18 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop18") == 1)
		{
			unequipItem18.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop18") != 1 && PlayerPrefs.GetInt("BuyShop18") == 1)
		{
			unequipItem18.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop18") == 1)
		{
			infoItem18.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop19") == 1)
		{
			buyItem19.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			equipItem19.SetActive(value: false);
			CharacterController.sparkle19 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop19") != 1 && PlayerPrefs.GetInt("BuyShop19") == 1)
		{
			equipItem19.SetActive(value: true);
			CharacterController.sparkle19 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop19") == 1)
		{
			unequipItem19.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop19") != 1 && PlayerPrefs.GetInt("BuyShop19") == 1)
		{
			unequipItem19.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop19") == 1)
		{
			infoItem19.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop20") == 1)
		{
			buyItem20.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			equipItem20.SetActive(value: false);
			CharacterController.sparkle20 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop20") != 1 && PlayerPrefs.GetInt("BuyShop20") == 1)
		{
			equipItem20.SetActive(value: true);
			CharacterController.sparkle20 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop20") == 1)
		{
			unequipItem20.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop20") != 1 && PlayerPrefs.GetInt("BuyShop20") == 1)
		{
			unequipItem20.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop20") == 1)
		{
			infoItem20.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("BuyShop21") == 1)
		{
			buyItem21.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			equipItem21.SetActive(value: false);
			CharacterController.sparkle21 = true;
		}
		if (PlayerPrefs.GetInt("EquipShop21") != 1 && PlayerPrefs.GetInt("BuyShop21") == 1)
		{
			equipItem21.SetActive(value: true);
			CharacterController.sparkle21 = false;
		}
		if (PlayerPrefs.GetInt("UnequipShop21") == 1)
		{
			unequipItem21.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("UnequipShop21") != 1 && PlayerPrefs.GetInt("BuyShop21") == 1)
		{
			unequipItem21.SetActive(value: true);
		}
		if (PlayerPrefs.GetInt("ItemShop21") == 1)
		{
			infoItem21.SetActive(value: false);
		}
	}

	public void FirstPage()
	{
		StartCoroutine(Transition1());
	}

	public void SecoundPage()
	{
		StartCoroutine(Transition2());
	}

	public void ThirdPage()
	{
		StartCoroutine(Transition3());
	}

	public void FourthPage()
	{
		StartCoroutine(Transition4());
	}

	public void FifthPage()
	{
		StartCoroutine(Transition5());
	}

	public void SixthPage()
	{
		StartCoroutine(Transition6());
	}

	public void SeventhPage()
	{
		StartCoroutine(Transition7());
	}

	private IEnumerator Transition1()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneAnim.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		sector1.SetActive(value: true);
		sector2.SetActive(value: false);
		sector3.SetActive(value: false);
		sector4.SetActive(value: false);
		sector5.SetActive(value: false);
		sector6.SetActive(value: false);
		sector7.SetActive(value: false);
		backMenu.SetActive(value: false);
		bothMenu.SetActive(value: false);
		bothMenu2.SetActive(value: false);
		bothMenu3.SetActive(value: false);
		bothMenu4.SetActive(value: false);
		bothMenu5.SetActive(value: false);
		nextMenu.SetActive(value: true);
		sceneAnim.SetTrigger("Open");
	}

	private IEnumerator Transition2()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneAnim.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		sector1.SetActive(value: false);
		sector2.SetActive(value: true);
		sector3.SetActive(value: false);
		sector4.SetActive(value: false);
		sector5.SetActive(value: false);
		sector6.SetActive(value: false);
		sector7.SetActive(value: false);
		backMenu.SetActive(value: false);
		bothMenu.SetActive(value: true);
		bothMenu2.SetActive(value: false);
		bothMenu3.SetActive(value: false);
		bothMenu4.SetActive(value: false);
		bothMenu5.SetActive(value: false);
		nextMenu.SetActive(value: false);
		sceneAnim.SetTrigger("Open");
	}

	private IEnumerator Transition3()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneAnim.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		sector1.SetActive(value: false);
		sector2.SetActive(value: false);
		sector3.SetActive(value: true);
		sector4.SetActive(value: false);
		sector5.SetActive(value: false);
		sector6.SetActive(value: false);
		sector7.SetActive(value: false);
		backMenu.SetActive(value: false);
		bothMenu.SetActive(value: false);
		bothMenu2.SetActive(value: true);
		bothMenu3.SetActive(value: false);
		bothMenu4.SetActive(value: false);
		bothMenu5.SetActive(value: false);
		nextMenu.SetActive(value: false);
		sceneAnim.SetTrigger("Open");
	}

	private IEnumerator Transition4()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneAnim.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		sector1.SetActive(value: false);
		sector2.SetActive(value: false);
		sector3.SetActive(value: false);
		sector4.SetActive(value: true);
		sector5.SetActive(value: false);
		sector6.SetActive(value: false);
		sector7.SetActive(value: false);
		backMenu.SetActive(value: false);
		bothMenu.SetActive(value: false);
		bothMenu2.SetActive(value: false);
		bothMenu3.SetActive(value: true);
		bothMenu4.SetActive(value: false);
		bothMenu5.SetActive(value: false);
		nextMenu.SetActive(value: false);
		sceneAnim.SetTrigger("Open");
	}

	private IEnumerator Transition5()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneAnim.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		sector1.SetActive(value: false);
		sector2.SetActive(value: false);
		sector3.SetActive(value: false);
		sector4.SetActive(value: false);
		sector5.SetActive(value: true);
		sector6.SetActive(value: false);
		sector7.SetActive(value: false);
		backMenu.SetActive(value: false);
		bothMenu.SetActive(value: false);
		bothMenu2.SetActive(value: false);
		bothMenu3.SetActive(value: false);
		bothMenu4.SetActive(value: true);
		bothMenu5.SetActive(value: false);
		nextMenu.SetActive(value: false);
		sceneAnim.SetTrigger("Open");
	}

	private IEnumerator Transition6()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneAnim.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		sector1.SetActive(value: false);
		sector2.SetActive(value: false);
		sector3.SetActive(value: false);
		sector4.SetActive(value: false);
		sector5.SetActive(value: false);
		sector6.SetActive(value: true);
		sector7.SetActive(value: false);
		backMenu.SetActive(value: false);
		bothMenu.SetActive(value: false);
		bothMenu2.SetActive(value: false);
		bothMenu3.SetActive(value: false);
		bothMenu4.SetActive(value: false);
		bothMenu5.SetActive(value: true);
		nextMenu.SetActive(value: false);
		sceneAnim.SetTrigger("Open");
	}

	private IEnumerator Transition7()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		sceneAnim.SetTrigger("Close");
		yield return new WaitForSeconds(1f);
		sector1.SetActive(value: false);
		sector2.SetActive(value: false);
		sector3.SetActive(value: false);
		sector4.SetActive(value: false);
		sector5.SetActive(value: false);
		sector6.SetActive(value: false);
		sector7.SetActive(value: true);
		backMenu.SetActive(value: true);
		bothMenu.SetActive(value: false);
		bothMenu2.SetActive(value: false);
		bothMenu3.SetActive(value: false);
		bothMenu4.SetActive(value: false);
		bothMenu5.SetActive(value: false);
		nextMenu.SetActive(value: false);
		sceneAnim.SetTrigger("Open");
	}

	public void ButterflyBuyCoins1()
	{
		if (butterflyCoinPrice1 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= butterflyCoinPrice1;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			butterflyCoinBuyItem1.SetActive(value: false);
			butterflyDiamondBuyItem1.SetActive(value: false);
			butterflyEquipItem1.SetActive(value: true);
			butterflyInfoItem1.SetActive(value: false);
			PlayerPrefs.SetInt("ButterflyBuyShop1", 1);
			PlayerPrefs.SetInt("ButterflyEquipShop1", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop1", 1);
			PlayerPrefs.SetInt("ButterflyItemShop1", 1);
			PlayerPrefs.Save();
		}
	}

	public void ButterflyBuyDiamonds1()
	{
		if (butterflyDiamondPrice1 <= PlayerPrefs.GetInt("TotalDiamonds"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalDiamonds -= butterflyDiamondPrice1;
			PlayerPrefs.SetInt("TotalDiamonds", CharacterController.totalDiamonds);
			butterflyCoinBuyItem1.SetActive(value: false);
			butterflyDiamondBuyItem1.SetActive(value: false);
			butterflyEquipItem1.SetActive(value: true);
			butterflyInfoItem1.SetActive(value: false);
			PlayerPrefs.SetInt("ButterflyBuyShop1", 1);
			PlayerPrefs.SetInt("ButterflyEquipShop1", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop1", 1);
			PlayerPrefs.SetInt("ButterflyItemShop1", 1);
			PlayerPrefs.Save();
		}
	}

	public void ButterflyEquip1()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		butterflyEquipItem1.SetActive(value: false);
		butterflyUnequipItem1.SetActive(value: true);
		PlayerPrefs.SetInt("ButterflyEquipShop1", 1);
		PlayerPrefs.SetInt("ButterflyUnequipShop1", 2);
		if (PlayerPrefs.GetInt("ButterflyEquipShop2") == 1)
		{
			PlayerPrefs.SetInt("ButterflyEquipShop2", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("ButterflyEquipShop3") == 1)
		{
			PlayerPrefs.SetInt("ButterflyEquipShop3", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop3", 1);
		}
		PlayerPrefs.Save();
	}

	public void ButterflyUnequip1()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		butterflyUnequipItem1.SetActive(value: false);
		butterflyEquipItem1.SetActive(value: true);
		PlayerPrefs.SetInt("ButterflyEquipShop1", 2);
		PlayerPrefs.SetInt("ButterflyUnequipShop1", 1);
		PlayerPrefs.Save();
	}

	public void ButterflyBuyCoins2()
	{
		if (butterflyCoinPrice2 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= butterflyCoinPrice2;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			butterflyCoinBuyItem2.SetActive(value: false);
			butterflyDiamondBuyItem2.SetActive(value: false);
			butterflyEquipItem2.SetActive(value: true);
			butterflyInfoItem2.SetActive(value: false);
			PlayerPrefs.SetInt("ButterflyBuyShop2", 1);
			PlayerPrefs.SetInt("ButterflyEquipShop2", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop2", 1);
			PlayerPrefs.SetInt("ButterflyItemShop2", 1);
			PlayerPrefs.Save();
		}
	}

	public void ButterflyBuyDiamonds2()
	{
		if (butterflyDiamondPrice2 <= PlayerPrefs.GetInt("TotalDiamonds"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalDiamonds -= butterflyDiamondPrice2;
			PlayerPrefs.SetInt("TotalDiamonds", CharacterController.totalDiamonds);
			butterflyCoinBuyItem2.SetActive(value: false);
			butterflyDiamondBuyItem2.SetActive(value: false);
			butterflyEquipItem2.SetActive(value: true);
			butterflyInfoItem2.SetActive(value: false);
			PlayerPrefs.SetInt("ButterflyBuyShop2", 1);
			PlayerPrefs.SetInt("ButterflyEquipShop2", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop2", 1);
			PlayerPrefs.SetInt("ButterflyItemShop2", 1);
			PlayerPrefs.Save();
		}
	}

	public void ButterflyEquip2()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		butterflyEquipItem2.SetActive(value: false);
		butterflyUnequipItem2.SetActive(value: true);
		PlayerPrefs.SetInt("ButterflyEquipShop2", 1);
		PlayerPrefs.SetInt("ButterflyUnequipShop2", 2);
		if (PlayerPrefs.GetInt("ButterflyEquipShop1") == 1)
		{
			PlayerPrefs.SetInt("ButterflyEquipShop1", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("ButterflyEquipShop3") == 1)
		{
			PlayerPrefs.SetInt("ButterflyEquipShop3", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop3", 1);
		}
		PlayerPrefs.Save();
	}

	public void ButterflyUnequip2()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		butterflyUnequipItem2.SetActive(value: false);
		butterflyEquipItem2.SetActive(value: true);
		PlayerPrefs.SetInt("ButterflyEquipShop2", 2);
		PlayerPrefs.SetInt("ButterflyUnequipShop2", 1);
		PlayerPrefs.Save();
	}

	public void ButterflyBuyCoins3()
	{
		if (butterflyCoinPrice3 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= butterflyCoinPrice3;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			butterflyCoinBuyItem3.SetActive(value: false);
			butterflyDiamondBuyItem3.SetActive(value: false);
			butterflyEquipItem3.SetActive(value: true);
			butterflyInfoItem3.SetActive(value: false);
			PlayerPrefs.SetInt("ButterflyBuyShop3", 1);
			PlayerPrefs.SetInt("ButterflyEquipShop3", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop3", 1);
			PlayerPrefs.SetInt("ButterflyItemShop3", 1);
			PlayerPrefs.Save();
		}
	}

	public void ButterflyBuyDiamonds3()
	{
		if (butterflyDiamondPrice3 <= PlayerPrefs.GetInt("TotalDiamonds"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalDiamonds -= butterflyDiamondPrice3;
			PlayerPrefs.SetInt("TotalDiamonds", CharacterController.totalDiamonds);
			butterflyCoinBuyItem3.SetActive(value: false);
			butterflyDiamondBuyItem3.SetActive(value: false);
			butterflyEquipItem3.SetActive(value: true);
			butterflyInfoItem3.SetActive(value: false);
			PlayerPrefs.SetInt("ButterflyBuyShop3", 1);
			PlayerPrefs.SetInt("ButterflyEquipShop3", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop3", 1);
			PlayerPrefs.SetInt("ButterflyItemShop3", 1);
			PlayerPrefs.Save();
		}
	}

	public void ButterflyEquip3()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		butterflyEquipItem3.SetActive(value: false);
		butterflyUnequipItem3.SetActive(value: true);
		PlayerPrefs.SetInt("ButterflyEquipShop3", 1);
		PlayerPrefs.SetInt("ButterflyUnequipShop3", 2);
		if (PlayerPrefs.GetInt("ButterflyEquipShop1") == 1)
		{
			PlayerPrefs.SetInt("ButterflyEquipShop1", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("ButterflyEquipShop2") == 1)
		{
			PlayerPrefs.SetInt("ButterflyEquipShop2", 2);
			PlayerPrefs.SetInt("ButterflyUnequipShop2", 1);
		}
		PlayerPrefs.Save();
	}

	public void ButterflyUnequip3()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		butterflyUnequipItem3.SetActive(value: false);
		butterflyEquipItem3.SetActive(value: true);
		PlayerPrefs.SetInt("ButterflyEquipShop3", 2);
		PlayerPrefs.SetInt("ButterflyUnequipShop3", 1);
		PlayerPrefs.Save();
	}

	public void BearBuyCoins1()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		if (bearCoinPrice1 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			CharacterController.totalCoins -= bearCoinPrice1;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			bearCoinBuyItem1.SetActive(value: false);
			bearDiamondBuyItem1.SetActive(value: false);
			bearEquipItem1.SetActive(value: true);
			bearInfoItem1.SetActive(value: false);
			PlayerPrefs.SetInt("BearBuyShop1", 1);
			PlayerPrefs.SetInt("BearEquipShop1", 2);
			PlayerPrefs.SetInt("BearUnequipShop1", 1);
			PlayerPrefs.SetInt("BearItemShop1", 1);
			PlayerPrefs.Save();
		}
	}

	public void BearBuyDiamonds1()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		if (bearDiamondPrice1 <= PlayerPrefs.GetInt("TotalDiamonds"))
		{
			CharacterController.totalDiamonds -= bearDiamondPrice1;
			PlayerPrefs.SetInt("TotalDiamonds", CharacterController.totalDiamonds);
			bearCoinBuyItem1.SetActive(value: false);
			bearDiamondBuyItem1.SetActive(value: false);
			bearEquipItem1.SetActive(value: true);
			bearInfoItem1.SetActive(value: false);
			PlayerPrefs.SetInt("BearBuyShop1", 1);
			PlayerPrefs.SetInt("BearEquipShop1", 2);
			PlayerPrefs.SetInt("BearUnequipShop1", 1);
			PlayerPrefs.SetInt("BearItemShop1", 1);
			PlayerPrefs.Save();
		}
	}

	public void BearEquip1()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		bearEquipItem1.SetActive(value: false);
		bearUnequipItem1.SetActive(value: true);
		PlayerPrefs.SetInt("BearEquipShop1", 1);
		PlayerPrefs.SetInt("BearUnequipShop1", 2);
		if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
		{
			PlayerPrefs.SetInt("BearEquipShop2", 2);
			PlayerPrefs.SetInt("BearUnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
		{
			PlayerPrefs.SetInt("BearEquipShop3", 2);
			PlayerPrefs.SetInt("BearUnequipShop3", 1);
		}
		PlayerPrefs.Save();
	}

	public void BearUnequip1()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		bearUnequipItem1.SetActive(value: false);
		bearEquipItem1.SetActive(value: true);
		PlayerPrefs.SetInt("BearEquipShop1", 2);
		PlayerPrefs.SetInt("BearUnequipShop1", 1);
		PlayerPrefs.Save();
	}

	public void BearBuyCoins2()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		if (bearCoinPrice2 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			CharacterController.totalCoins -= bearCoinPrice2;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			bearCoinBuyItem2.SetActive(value: false);
			bearDiamondBuyItem2.SetActive(value: false);
			bearEquipItem2.SetActive(value: true);
			bearInfoItem2.SetActive(value: false);
			PlayerPrefs.SetInt("BearBuyShop2", 1);
			PlayerPrefs.SetInt("BearEquipShop2", 2);
			PlayerPrefs.SetInt("BearUnequipShop2", 1);
			PlayerPrefs.SetInt("BearItemShop2", 1);
			PlayerPrefs.Save();
		}
	}

	public void BearBuyDiamonds2()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		if (bearDiamondPrice2 <= PlayerPrefs.GetInt("TotalDiamonds"))
		{
			CharacterController.totalDiamonds -= bearDiamondPrice2;
			PlayerPrefs.SetInt("TotalDiamonds", CharacterController.totalDiamonds);
			bearCoinBuyItem2.SetActive(value: false);
			bearDiamondBuyItem2.SetActive(value: false);
			bearEquipItem2.SetActive(value: true);
			bearInfoItem2.SetActive(value: false);
			PlayerPrefs.SetInt("BearBuyShop2", 1);
			PlayerPrefs.SetInt("BearEquipShop2", 2);
			PlayerPrefs.SetInt("BearUnequipShop2", 1);
			PlayerPrefs.SetInt("BearItemShop2", 1);
			PlayerPrefs.Save();
		}
	}

	public void BearEquip2()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		bearEquipItem2.SetActive(value: false);
		bearUnequipItem2.SetActive(value: true);
		PlayerPrefs.SetInt("BearEquipShop2", 1);
		PlayerPrefs.SetInt("BearUnequipShop2", 2);
		if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
		{
			PlayerPrefs.SetInt("BearEquipShop1", 2);
			PlayerPrefs.SetInt("BearUnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("BearEquipShop3") == 1)
		{
			PlayerPrefs.SetInt("BearEquipShop3", 2);
			PlayerPrefs.SetInt("BearUnequipShop3", 1);
		}
		PlayerPrefs.Save();
	}

	public void BearUnequip2()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		bearUnequipItem2.SetActive(value: false);
		bearEquipItem2.SetActive(value: true);
		PlayerPrefs.SetInt("BearEquipShop2", 2);
		PlayerPrefs.SetInt("BearUnequipShop2", 1);
		PlayerPrefs.Save();
	}

	public void BearBuyCoins3()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		if (bearCoinPrice3 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			CharacterController.totalCoins -= bearCoinPrice3;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			bearCoinBuyItem3.SetActive(value: false);
			bearDiamondBuyItem3.SetActive(value: false);
			bearEquipItem3.SetActive(value: true);
			bearInfoItem3.SetActive(value: false);
			PlayerPrefs.SetInt("BearBuyShop3", 1);
			PlayerPrefs.SetInt("BearEquipShop3", 2);
			PlayerPrefs.SetInt("BearUnequipShop3", 1);
			PlayerPrefs.SetInt("BearItemShop3", 1);
			PlayerPrefs.Save();
		}
	}

	public void BearBuyDiamonds3()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		if (bearDiamondPrice3 <= PlayerPrefs.GetInt("TotalDiamonds"))
		{
			CharacterController.totalDiamonds -= bearDiamondPrice3;
			PlayerPrefs.SetInt("TotalDiamonds", CharacterController.totalDiamonds);
			bearCoinBuyItem3.SetActive(value: false);
			bearDiamondBuyItem3.SetActive(value: false);
			bearEquipItem3.SetActive(value: true);
			bearInfoItem3.SetActive(value: false);
			PlayerPrefs.SetInt("BearBuyShop3", 1);
			PlayerPrefs.SetInt("BearEquipShop3", 2);
			PlayerPrefs.SetInt("BearUnequipShop3", 1);
			PlayerPrefs.SetInt("BearItemShop3", 1);
			PlayerPrefs.Save();
		}
	}

	public void BearEquip3()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		bearEquipItem3.SetActive(value: false);
		bearUnequipItem3.SetActive(value: true);
		PlayerPrefs.SetInt("BearEquipShop3", 1);
		PlayerPrefs.SetInt("BearUnequipShop3", 2);
		if (PlayerPrefs.GetInt("BearEquipShop1") == 1)
		{
			PlayerPrefs.SetInt("BearEquipShop1", 2);
			PlayerPrefs.SetInt("BearUnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("BearEquipShop2") == 1)
		{
			PlayerPrefs.SetInt("BearEquipShop2", 2);
			PlayerPrefs.SetInt("BearUnequipShop2", 1);
		}
		PlayerPrefs.Save();
	}

	public void BearUnequip3()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		bearUnequipItem3.SetActive(value: false);
		bearEquipItem3.SetActive(value: true);
		PlayerPrefs.SetInt("BearEquipShop3", 2);
		PlayerPrefs.SetInt("BearUnequipShop3", 1);
		PlayerPrefs.Save();
	}

	public void Buy1()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		if (price1 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			CharacterController.totalCoins -= price1;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem1.SetActive(value: false);
			equipItem1.SetActive(value: true);
			infoItem1.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop1", 1);
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
			PlayerPrefs.SetInt("ItemShop1", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip1()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem1.SetActive(value: false);
		unequipItem1.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop1", 1);
		PlayerPrefs.SetInt("UnequipShop1", 2);
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip1()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem1.SetActive(value: false);
		equipItem1.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop1", 2);
		PlayerPrefs.SetInt("UnequipShop1", 1);
		PlayerPrefs.Save();
	}

	public void Buy2()
	{
		if (price2 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price2;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem2.SetActive(value: false);
			equipItem2.SetActive(value: true);
			infoItem2.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop2", 1);
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
			PlayerPrefs.SetInt("ItemShop2", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip2()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem2.SetActive(value: false);
		unequipItem2.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop2", 1);
		PlayerPrefs.SetInt("UnequipShop2", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip2()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem2.SetActive(value: false);
		equipItem2.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop2", 2);
		PlayerPrefs.SetInt("UnequipShop2", 1);
		PlayerPrefs.Save();
	}

	public void Buy3()
	{
		if (price3 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price3;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem3.SetActive(value: false);
			equipItem3.SetActive(value: true);
			infoItem3.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop3", 1);
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
			PlayerPrefs.SetInt("ItemShop3", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip3()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem3.SetActive(value: false);
		unequipItem3.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop3", 1);
		PlayerPrefs.SetInt("UnequipShop3", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip3()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem3.SetActive(value: false);
		equipItem3.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop3", 2);
		PlayerPrefs.SetInt("UnequipShop3", 1);
		PlayerPrefs.Save();
	}

	public void Buy4()
	{
		if (price4 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price4;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem4.SetActive(value: false);
			equipItem4.SetActive(value: true);
			infoItem4.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop4", 1);
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
			PlayerPrefs.SetInt("ItemShop4", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip4()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem4.SetActive(value: false);
		unequipItem4.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop4", 1);
		PlayerPrefs.SetInt("UnequipShop4", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip4()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem4.SetActive(value: false);
		equipItem4.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop4", 2);
		PlayerPrefs.SetInt("UnequipShop4", 1);
		PlayerPrefs.Save();
	}

	public void Buy5()
	{
		if (price5 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price5;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem5.SetActive(value: false);
			equipItem5.SetActive(value: true);
			infoItem5.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop5", 1);
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
			PlayerPrefs.SetInt("ItemShop5", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip5()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem5.SetActive(value: false);
		unequipItem5.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop5", 1);
		PlayerPrefs.SetInt("UnequipShop5", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip5()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem5.SetActive(value: false);
		equipItem5.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop5", 2);
		PlayerPrefs.SetInt("UnequipShop5", 1);
		PlayerPrefs.Save();
	}

	public void Buy6()
	{
		if (price6 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price6;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem6.SetActive(value: false);
			equipItem6.SetActive(value: true);
			infoItem6.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop6", 1);
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
			PlayerPrefs.SetInt("ItemShop6", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip6()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem6.SetActive(value: false);
		unequipItem6.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop6", 1);
		PlayerPrefs.SetInt("UnequipShop6", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip6()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem6.SetActive(value: false);
		equipItem6.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop6", 2);
		PlayerPrefs.SetInt("UnequipShop6", 1);
		PlayerPrefs.Save();
	}

	public void Buy7()
	{
		if (price7 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price7;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem7.SetActive(value: false);
			equipItem7.SetActive(value: true);
			infoItem7.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop7", 1);
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
			PlayerPrefs.SetInt("ItemShop7", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip7()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem7.SetActive(value: false);
		unequipItem7.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop7", 1);
		PlayerPrefs.SetInt("UnequipShop7", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip7()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem7.SetActive(value: false);
		equipItem7.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop7", 2);
		PlayerPrefs.SetInt("UnequipShop7", 1);
		PlayerPrefs.Save();
	}

	public void Buy8()
	{
		if (price8 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price8;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem8.SetActive(value: false);
			equipItem8.SetActive(value: true);
			infoItem8.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop8", 1);
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
			PlayerPrefs.SetInt("ItemShop8", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip8()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem8.SetActive(value: false);
		unequipItem8.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop8", 1);
		PlayerPrefs.SetInt("UnequipShop8", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip8()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem8.SetActive(value: false);
		equipItem8.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop8", 2);
		PlayerPrefs.SetInt("UnequipShop8", 1);
		PlayerPrefs.Save();
	}

	public void Buy9()
	{
		if (price9 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price9;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem9.SetActive(value: false);
			equipItem9.SetActive(value: true);
			infoItem1.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop9", 1);
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
			PlayerPrefs.SetInt("ItemShop9", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip9()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem9.SetActive(value: false);
		unequipItem9.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop9", 1);
		PlayerPrefs.SetInt("UnequipShop9", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip9()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem9.SetActive(value: false);
		equipItem9.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop9", 2);
		PlayerPrefs.SetInt("UnequipShop9", 1);
		PlayerPrefs.Save();
	}

	public void Buy10()
	{
		if (price10 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price10;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem10.SetActive(value: false);
			equipItem10.SetActive(value: true);
			infoItem10.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop10", 1);
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
			PlayerPrefs.SetInt("ItemShop10", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip10()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem10.SetActive(value: false);
		unequipItem10.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop10", 1);
		PlayerPrefs.SetInt("UnequipShop10", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip10()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem10.SetActive(value: false);
		equipItem10.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop10", 2);
		PlayerPrefs.SetInt("UnequipShop10", 1);
		PlayerPrefs.Save();
	}

	public void Buy11()
	{
		if (price11 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price11;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem11.SetActive(value: false);
			equipItem11.SetActive(value: true);
			infoItem11.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop11", 1);
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
			PlayerPrefs.SetInt("ItemShop11", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip11()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem11.SetActive(value: false);
		unequipItem11.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop11", 1);
		PlayerPrefs.SetInt("UnequipShop11", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip11()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem11.SetActive(value: false);
		equipItem11.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop11", 2);
		PlayerPrefs.SetInt("UnequipShop11", 1);
		PlayerPrefs.Save();
	}

	public void Buy12()
	{
		if (price12 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price12;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem12.SetActive(value: false);
			equipItem12.SetActive(value: true);
			infoItem12.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop12", 1);
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
			PlayerPrefs.SetInt("ItemShop12", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip12()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem12.SetActive(value: false);
		unequipItem12.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop12", 1);
		PlayerPrefs.SetInt("UnequipShop12", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip12()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem12.SetActive(value: false);
		equipItem12.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop12", 2);
		PlayerPrefs.SetInt("UnequipShop12", 1);
		PlayerPrefs.Save();
	}

	public void Buy13()
	{
		if (price13 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price13;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem13.SetActive(value: false);
			equipItem13.SetActive(value: true);
			infoItem13.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop13", 1);
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
			PlayerPrefs.SetInt("ItemShop13", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip13()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem13.SetActive(value: false);
		unequipItem13.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop13", 1);
		PlayerPrefs.SetInt("UnequipShop13", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip13()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem13.SetActive(value: false);
		equipItem13.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop13", 2);
		PlayerPrefs.SetInt("UnequipShop13", 1);
		PlayerPrefs.Save();
	}

	public void Buy14()
	{
		if (price14 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price14;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem14.SetActive(value: false);
			equipItem14.SetActive(value: true);
			infoItem14.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop14", 1);
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
			PlayerPrefs.SetInt("ItemShop14", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip14()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem14.SetActive(value: false);
		unequipItem14.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop14", 1);
		PlayerPrefs.SetInt("UnequipShop14", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip14()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem14.SetActive(value: false);
		equipItem14.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop14", 2);
		PlayerPrefs.SetInt("UnequipShop14", 1);
		PlayerPrefs.Save();
	}

	public void Buy15()
	{
		if (price15 <= PlayerPrefs.GetInt("TotalDiamonds"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalDiamonds -= price15;
			PlayerPrefs.SetInt("TotalDiamonds", CharacterController.totalDiamonds);
			buyItem15.SetActive(value: false);
			equipItem15.SetActive(value: true);
			infoItem15.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop15", 1);
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
			PlayerPrefs.SetInt("ItemShop15", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip15()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem15.SetActive(value: false);
		unequipItem15.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop15", 1);
		PlayerPrefs.SetInt("UnequipShop15", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip15()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem15.SetActive(value: false);
		equipItem15.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop15", 2);
		PlayerPrefs.SetInt("UnequipShop15", 1);
		PlayerPrefs.Save();
	}

	public void Buy16()
	{
		if (price16 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price16;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem16.SetActive(value: false);
			equipItem16.SetActive(value: true);
			infoItem16.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop16", 1);
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
			PlayerPrefs.SetInt("ItemShop16", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip16()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem16.SetActive(value: false);
		unequipItem16.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop16", 1);
		PlayerPrefs.SetInt("UnequipShop16", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip16()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem16.SetActive(value: false);
		equipItem16.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop16", 2);
		PlayerPrefs.SetInt("UnequipShop16", 1);
		PlayerPrefs.Save();
	}

	public void Buy17()
	{
		if (price17 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price17;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem17.SetActive(value: false);
			equipItem17.SetActive(value: true);
			infoItem17.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop17", 1);
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
			PlayerPrefs.SetInt("ItemShop17", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip17()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem17.SetActive(value: false);
		unequipItem17.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop17", 1);
		PlayerPrefs.SetInt("UnequipShop17", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip17()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem17.SetActive(value: false);
		equipItem17.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop17", 2);
		PlayerPrefs.SetInt("UnequipShop17", 1);
		PlayerPrefs.Save();
	}

	public void Buy18()
	{
		if (price18 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price18;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem18.SetActive(value: false);
			equipItem18.SetActive(value: true);
			infoItem18.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop18", 1);
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
			PlayerPrefs.SetInt("ItemShop18", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip18()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem18.SetActive(value: false);
		unequipItem18.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop18", 1);
		PlayerPrefs.SetInt("UnequipShop18", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip18()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem18.SetActive(value: false);
		equipItem18.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop18", 2);
		PlayerPrefs.SetInt("UnequipShop18", 1);
		PlayerPrefs.Save();
	}

	public void Buy19()
	{
		if (price19 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price19;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem19.SetActive(value: false);
			equipItem19.SetActive(value: true);
			infoItem19.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop19", 1);
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
			PlayerPrefs.SetInt("ItemShop19", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip19()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem19.SetActive(value: false);
		unequipItem19.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop19", 1);
		PlayerPrefs.SetInt("UnequipShop19", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip19()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem19.SetActive(value: false);
		equipItem19.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop19", 2);
		PlayerPrefs.SetInt("UnequipShop19", 1);
		PlayerPrefs.Save();
	}

	public void Buy20()
	{
		if (price20 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price20;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem20.SetActive(value: false);
			equipItem20.SetActive(value: true);
			infoItem20.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop20", 1);
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
			PlayerPrefs.SetInt("ItemShop20", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip20()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem20.SetActive(value: false);
		unequipItem20.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop20", 1);
		PlayerPrefs.SetInt("UnequipShop20", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop21") == 1)
		{
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip20()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem20.SetActive(value: false);
		equipItem20.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop20", 2);
		PlayerPrefs.SetInt("UnequipShop20", 1);
		PlayerPrefs.Save();
	}

	public void Buy21()
	{
		if (price21 <= PlayerPrefs.GetInt("TotalCoins"))
		{
			Object.FindObjectOfType<AudioManager>().Play("ClickSound");
			CharacterController.totalCoins -= price21;
			PlayerPrefs.SetInt("TotalCoins", CharacterController.totalCoins);
			buyItem21.SetActive(value: false);
			equipItem21.SetActive(value: true);
			infoItem21.SetActive(value: false);
			PlayerPrefs.SetInt("BuyShop21", 1);
			PlayerPrefs.SetInt("EquipShop21", 2);
			PlayerPrefs.SetInt("UnequipShop21", 1);
			PlayerPrefs.SetInt("ItemShop21", 1);
			PlayerPrefs.Save();
		}
	}

	public void Equip21()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		equipItem21.SetActive(value: false);
		unequipItem21.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop21", 1);
		PlayerPrefs.SetInt("UnequipShop21", 2);
		if (PlayerPrefs.GetInt("EquipShop1") == 1)
		{
			PlayerPrefs.SetInt("EquipShop1", 2);
			PlayerPrefs.SetInt("UnequipShop1", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop2") == 1)
		{
			PlayerPrefs.SetInt("EquipShop2", 2);
			PlayerPrefs.SetInt("UnequipShop2", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop3") == 1)
		{
			PlayerPrefs.SetInt("EquipShop3", 2);
			PlayerPrefs.SetInt("UnequipShop3", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop4") == 1)
		{
			PlayerPrefs.SetInt("EquipShop4", 2);
			PlayerPrefs.SetInt("UnequipShop4", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop5") == 1)
		{
			PlayerPrefs.SetInt("EquipShop5", 2);
			PlayerPrefs.SetInt("UnequipShop5", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop6") == 1)
		{
			PlayerPrefs.SetInt("EquipShop6", 2);
			PlayerPrefs.SetInt("UnequipShop6", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop7") == 1)
		{
			PlayerPrefs.SetInt("EquipShop7", 2);
			PlayerPrefs.SetInt("UnequipShop7", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop8") == 1)
		{
			PlayerPrefs.SetInt("EquipShop8", 2);
			PlayerPrefs.SetInt("UnequipShop8", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop9") == 1)
		{
			PlayerPrefs.SetInt("EquipShop9", 2);
			PlayerPrefs.SetInt("UnequipShop9", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop10") == 1)
		{
			PlayerPrefs.SetInt("EquipShop10", 2);
			PlayerPrefs.SetInt("UnequipShop10", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop11") == 1)
		{
			PlayerPrefs.SetInt("EquipShop11", 2);
			PlayerPrefs.SetInt("UnequipShop11", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop12") == 1)
		{
			PlayerPrefs.SetInt("EquipShop12", 2);
			PlayerPrefs.SetInt("UnequipShop12", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop13") == 1)
		{
			PlayerPrefs.SetInt("EquipShop13", 2);
			PlayerPrefs.SetInt("UnequipShop13", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop14") == 1)
		{
			PlayerPrefs.SetInt("EquipShop14", 2);
			PlayerPrefs.SetInt("UnequipShop14", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop15") == 1)
		{
			PlayerPrefs.SetInt("EquipShop15", 2);
			PlayerPrefs.SetInt("UnequipShop15", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop16") == 1)
		{
			PlayerPrefs.SetInt("EquipShop16", 2);
			PlayerPrefs.SetInt("UnequipShop16", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop17") == 1)
		{
			PlayerPrefs.SetInt("EquipShop17", 2);
			PlayerPrefs.SetInt("UnequipShop17", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop18") == 1)
		{
			PlayerPrefs.SetInt("EquipShop18", 2);
			PlayerPrefs.SetInt("UnequipShop18", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop19") == 1)
		{
			PlayerPrefs.SetInt("EquipShop19", 2);
			PlayerPrefs.SetInt("UnequipShop19", 1);
		}
		if (PlayerPrefs.GetInt("EquipShop20") == 1)
		{
			PlayerPrefs.SetInt("EquipShop20", 2);
			PlayerPrefs.SetInt("UnequipShop20", 1);
		}
		PlayerPrefs.Save();
	}

	public void Unequip21()
	{
		Object.FindObjectOfType<AudioManager>().Play("ClickSound");
		unequipItem21.SetActive(value: false);
		equipItem21.SetActive(value: true);
		PlayerPrefs.SetInt("EquipShop21", 2);
		PlayerPrefs.SetInt("UnequipShop21", 1);
		PlayerPrefs.Save();
	}
}
