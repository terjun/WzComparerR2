using System;
using System.Collections.Generic;
using System.Text;

namespace WzComparerR2.CharaSim
{
    public static class ItemStringHelper
    {
        /// <summary>
        /// 获取怪物category属性对应的类型说明。
        /// </summary>
        /// <param Name="category">怪物的category属性的值。</param>
        /// <returns></returns>
        public static string GetMobCategoryName(int category)
        {
            switch (category)
            {
                case 0: return "无形态";
                case 1: return "动物型";
                case 2: return "植物型";
                case 3: return "鱼类型";
                case 4: return "爬虫类型";
                case 5: return "精灵型";
                case 6: return "恶魔型";
                case 7: return "不死型";
                case 8: return "无机物型";
                default: return null;
            }
        }

        public static string GetGearPropString(GearPropType propType, int value)
        {
            return GetGearPropString(propType, value, 0);
        }

        /// <summary>
        /// 获取GearPropType所对应的文字说明。
        /// </summary>
        /// <param Name="propType">表示装备属性枚举GearPropType。</param>
        /// <param Name="Value">表示propType属性所对应的值。</param>
        /// <returns></returns>
        public static string GetGearPropString(GearPropType propType, int value, int signFlag)
        {

            string sign;
            switch (signFlag)
            {
                default:
                case 0: //默认处理符号
                    sign = value > 0 ? "+" : null;
                    break;

                case 1: //固定加号
                    sign = "+";
                    break;

                case 2: //无特别符号
                    sign = "";
                    break;
            }
            switch (propType)
            {
                case GearPropType.incSTR: return "STR : " + sign + value;
                case GearPropType.incSTRr: return "STR : " + sign + value + "%";
                case GearPropType.incDEX: return "DEX : " + sign + value;
                case GearPropType.incDEXr: return "DEX : " + sign + value + "%";
                case GearPropType.incINT: return "INT : " + sign + value;
                case GearPropType.incINTr: return "INT : " + sign + value + "%";
                case GearPropType.incLUK: return "LUK : " + sign + value;
                case GearPropType.incLUKr: return "LUK : " + sign + value + "%";
                case GearPropType.incAllStat: return "올스탯: " + sign + value;
                case GearPropType.incMHP: return "최대 HP : " + sign + value;
                case GearPropType.incMHPr: return "최대 HP : " + sign + value + "%";
                case GearPropType.incMMP: return "최대 MP : " + sign + value;
                case GearPropType.incMMPr: return "최대 MP : " + sign + value + "%";
                case GearPropType.incMDF: return "MaxDF : " + sign + value;
                case GearPropType.incPAD: return "공격력 : " + sign + value;
                case GearPropType.incPADr: return "공격력 : " + sign + value + "%";
                case GearPropType.incMAD: return "마력 : " + sign + value;
                case GearPropType.incMADr: return "마력 : " + sign + value + "%";
                case GearPropType.incPDD: return "방어력 : " + sign + value;
                case GearPropType.incPDDr: return "방어력 : " + sign + value + "%";
                //case GearPropType.incMDD: return "魔法防御力 : " + sign + value;
                //case GearPropType.incMDDr: return "魔法防御力 : " + sign + value + "%";
                //case GearPropType.incACC: return "命中值 : " + sign + value;
                //case GearPropType.incACCr: return "命中值 : " + sign + value + "%";
                //case GearPropType.incEVA: return "回避值 : " + sign + value;
                //case GearPropType.incEVAr: return "回避值 : " + sign + value + "%";
                case GearPropType.incSpeed: return "이동속도 : " + sign + value;
                case GearPropType.incJump: return "점프력 : " + sign + value;
                case GearPropType.incCraft: return "손재주 : " + sign + value;
                case GearPropType.damR:
                case GearPropType.incDAMr: return "데미지 : " + sign + value + "%";
                case GearPropType.incCr: return "爆击率 : " + sign + value + "%";
                case GearPropType.knockback: return "直接攻击时" + value + "的比率发生后退现象。";
                case GearPropType.incPVPDamage: return "大乱斗时追加攻击力" + sign + value;
                case GearPropType.incPQEXPr: return "组队任务经验值增加" + value + "%";
                case GearPropType.incBDR:
                case GearPropType.bdR: return "보스 공격시 데미지 +" + value + "%";
                case GearPropType.incIMDR:
                case GearPropType.imdR: return "몬스터 방어력 무시 : +" + value + "%";
                case GearPropType.limitBreak: return "伤害上限突破至" + value + "。";
                case GearPropType.reduceReq: return "착용 레벨 감소 : - " + value;

                case GearPropType.only: return value == 0 ? null : "고유 아이템";
                case GearPropType.tradeBlock: return value == 0 ? null : "교환 불가";
                case GearPropType.equipTradeBlock: return value == 0 ? null : "장착시 교환 불가";
                case GearPropType.accountSharable: return value == 0 ? null : "계정 내 이동만 가능";
                case GearPropType.onlyEquip: return value == 0 ? null : "고유 장착 아이템";
                case GearPropType.notExtend: return value == 0 ? null : "无法延长有效时间。";
                case GearPropType.tradeAvailable:
                    switch (value)
                    {
                        case 1: return " #c카르마의 가위를 사용하면 1회 교환이 가능하게 할 수 있습니다.#";
                        case 2: return " #c플래티넘 카르마의 가위를 사용하면 1회 교환이 가능하게 할 수 있습니다.#";
                        default: return null;
                    }
                case GearPropType.accountShareTag:
                    switch (value)
                    {
                        case 1: return " #c使用物品共享牌，可以在同一账号内的角色间移动1次。#";
                        default: return null;
                    }
                case GearPropType.noPotential: return value == 0 ? null : "잠재능력 설정 불가";
                case GearPropType.fixedPotential: return value == 0 ? null : "잠재능력 재설정 불가";
                case GearPropType.superiorEqp: return value == 0 ? null : "아이템 강화 성공시 더욱 높은 효과를 받을 수 있습니다.";
                case GearPropType.nActivatedSocket: return value == 0 ? null : "#c可以镶嵌星岩#";
                case GearPropType.jokerToSetItem: return value == 0 ? null : " #c어떠한 셋트 아이템에도 포함되는 럭키 아이템!#";

                case GearPropType.incMHP_incMMP: return "최대 HP / 최대 MP : " + sign + value;
                case GearPropType.incMHPr_incMMPr: return "최대 HP / 최대 MP : " + sign + value + "%";
                case GearPropType.incPAD_incMAD: return "공격력 / 마력 : " + sign + value;
                case GearPropType.incPDD_incMDD: return "방어력 : " + sign + value;
                //case GearPropType.incACC_incEVA: return "命中值/回避值：" + sign + value;

                case GearPropType.incARC: return "ARC : " + sign + value;
                default: return null;
            }
        }

        /// <summary>
        /// 获取gearGrade所对应的字符串。
        /// </summary>
        /// <param Name="rank">表示装备的潜能等级GearGrade。</param>
        /// <returns></returns>
        public static string GetGearGradeString(GearGrade rank)
        {
            switch (rank)
            {
                case GearGrade.C: return "C级(一般物品)";
                case GearGrade.B: return "B级(高级物品)";
                case GearGrade.A: return "A级(史诗物品)";
                case GearGrade.S: return "S级(传说物品)";
                case GearGrade.SS: return "SS级(传说极品)";
                case GearGrade.Special: return "(特殊物品)";
                default: return null;
            }
        }

        /// <summary>
        /// 获取gearType所对应的字符串。
        /// </summary>
        /// <param Name="Type">表示装备类型GearType。</param>
        /// <returns></returns>
        public static string GetGearTypeString(GearType type)
        {
            switch (type)
            {
                case GearType.body: return "纸娃娃(身体)";
                case GearType.head: return "纸娃娃(头部)";
                case GearType.face: return "纸娃娃(脸型)";
                case GearType.hair:
                case GearType.hair2: return "纸娃娃(发型)";
                case GearType.faceAccessory: return "얼굴장식";
                case GearType.eyeAccessory: return "눈장식";
                case GearType.earrings: return "귀걸이";
                case GearType.pendant: return "펜던트";
                case GearType.belt: return "벨트";
                case GearType.medal: return "훈장";
                case GearType.shoulderPad: return "어깨장식";
                case GearType.cap: return "모자";
                case GearType.cape: return "망토";
                case GearType.coat: return "상의";
                case GearType.dragonMask: return "드래곤 모자";
                case GearType.dragonPendant: return "드래곤 펜던트";
                case GearType.dragonWings: return "드래곤 날개장식";
                case GearType.dragonTail: return "드래곤 꼬리장식";
                case GearType.glove: return "장갑";
                case GearType.longcoat: return "한벌옷";
                case GearType.machineEngine: return "메카닉 엔진";
                case GearType.machineArms: return "메카닉 암";
                case GearType.machineLegs: return "메카닉 다리";
                case GearType.machineBody: return "메카닉 프레임";
                case GearType.machineTransistors: return "메카닉 트랜지스터";
                case GearType.pants: return "하의";
                case GearType.ring: return "반지";
                case GearType.shield: return "방패";
                case GearType.shoes: return "신발";
                case GearType.shiningRod: return "샤이닝로드";
                case GearType.soulShooter: return "소울슈터";
                case GearType.ohSword: return "한손검";
                case GearType.ohAxe: return "한손도끼";
                case GearType.ohBlunt: return "한손둔기";
                case GearType.dagger: return "단검";
                case GearType.katara: return "刀";
                case GearType.magicArrow: return "마법화살";
                case GearType.card: return "카드";
                case GearType.box: return "宝盒";
                case GearType.orb: return "오브";
                case GearType.novaMarrow: return "용의 정수";
                case GearType.soulBangle: return "소울링";
                case GearType.mailin: return "麦林";
                case GearType.cane: return "케인";
                case GearType.wand: return "완드";
                case GearType.staff: return "스태프";
                case GearType.thSword: return "두손검";
                case GearType.thAxe: return "두손도끼";
                case GearType.thBlunt: return "두손둔기";
                case GearType.spear: return "창";
                case GearType.polearm: return "폴암";
                case GearType.bow: return "활";
                case GearType.crossbow: return "석궁";
                case GearType.throwingGlove: return "아대";
                case GearType.knuckle: return "너클";
                case GearType.gun: return "총";
                case GearType.android: return "안드로이드";
                case GearType.machineHeart: return "기계심장";
                case GearType.pickaxe: return "채광 도구";
                case GearType.shovel: return "약초채집 도구";
                case GearType.pocket: return "포켓 아이템";
                case GearType.dualBow: return "듀얼보우건";
                case GearType.handCannon: return "핸드캐논";
                case GearType.badge: return "뱃지";
                case GearType.emblem: return "엠블렘";
                case GearType.soulShield: return "灵魂盾";
                case GearType.demonShield: return "精气盾";
                case GearType.totem: return "图腾";
                case GearType.petEquip: return "펫장비";
                case GearType.taming:
                case GearType.taming2:
                case GearType.taming3: 
                case GearType.tamingChair: return "骑兽";
                case GearType.saddle: return "鞍子";
                case GearType.katana: return "武士刀";
                case GearType.fan: return "折扇";
                case GearType.swordZB: return "대검";
                case GearType.swordZL: return "태도";
                case GearType.weapon: return "무기";
                case GearType.subWeapon: return "보조무기";
                case GearType.heroMedal: return "메달";
                case GearType.rosario: return "로자리오";
                case GearType.chain: return "쇠사슬";
                case GearType.book1:
                case GearType.book2:
                case GearType.book3: return "마도서";
                case GearType.bowMasterFeather: return "화살깃";
                case GearType.crossBowThimble: return "활골무";
                case GearType.shadowerSheath: return "단검용 검집";
                case GearType.nightLordPoutch: return "부적";
                case GearType.viperWristband: return "리스트밴드";
                case GearType.captainSight: return "조준기";
                case GearType.connonGunPowder: 
                case GearType.connonGunPowder2: return "화약통";
                case GearType.aranPendulum: return "무게추";
                case GearType.evanPaper: return "문서";
                case GearType.battlemageBall: return "마법구슬";
                case GearType.wildHunterArrowHead: return "화살촉";
                case GearType.cygnusGem: return "보석";
                case GearType.powerSource: return "파워소스";
                case GearType.foxPearl: return "여우 구슬";
                case GearType.chess: return "체스피스";

                case GearType.energySword: return "에너지소드";
                case GearType.desperado: return "데스페라도";
                case GearType.magicStick: return "驯兽魔法棒";
                case GearType.whistle: return "哨子";
                case GearType.boxingClaw: return "拳爪";
                case GearType.katana2: return "小太刀";
                case GearType.espLimiter: return "ESP 리미터";

                case GearType.GauntletBuster: return "건틀렛 리볼버";
                case GearType.ExplosivePill: return "장약";
                default: return null;
            }
        }

        /// <summary>
        /// 获取武器攻击速度所对应的字符串。
        /// </summary>
        /// <param Name="attackSpeed">表示武器的攻击速度，通常为2~9的数字。</param>
        /// <returns></returns>
        public static string GetAttackSpeedString(int attackSpeed)
        {
            switch (attackSpeed)
            {
                case 2:
                case 3: return "매우빠름";
                case 4:
                case 5: return "빠름";
                case 6: return "보통";
                case 7:
                case 8: return "느림";
                case 9: return "매우느림";
                default:
                    if (attackSpeed < 2) return "吃屎一样快";
                    else if (attackSpeed > 9) return "吃屎一样慢";
                    else return attackSpeed.ToString();
            }
        }

        /// <summary>
        /// 获取套装装备类型的字符串。
        /// </summary>
        /// <param Name="Type">表示套装装备类型的GearType。</param>
        /// <returns></returns>
        public static string GetSetItemGearTypeString(GearType type)
        {
            return GetGearTypeString(type);
        }

        /// <summary>
        /// 获取装备额外职业要求说明的字符串。
        /// </summary>
        /// <param Name="Type">表示装备类型的GearType。</param>
        /// <returns></returns>
        public static string GetExtraJobReqString(GearType type)
        {
            switch (type)
            {
                case GearType.katara: return "暗影双刀林之灵可以装备";
                case GearType.demonShield: return "데몬 직업군 착용 가능";
                case GearType.magicArrow: return "메르세데스 착용가능";
                case GearType.card: return "팬텀 착용 가능";
                case GearType.box: return "龙的传人可以装备";
                case GearType.orb:
                case GearType.shiningRod: return "루미너스 착용 가능";
                case GearType.novaMarrow: return "카이저 착용 가능";
                case GearType.soulBangle:
                case GearType.soulShooter: return "엔젤릭 버스터 착용 가능";
                case GearType.soulShield: return "미하일 착용 가능";
                case GearType.mailin: return "机械师可以装备";

                case GearType.heroMedal: return "히어로 직업군 착용 가능";
                case GearType.rosario: return "팔라딘 직업군 착용 가능";
                case GearType.chain: return "다크나이트 직업군 착용 가능";
                case GearType.book1: return "불,독 계열 마법사 착용 가능";
                case GearType.book2: return "얼음,번개 계열 마법사 착용 가능";
                case GearType.book3: return "비숍 계열 마법사 착용 가능";
                case GearType.bowMasterFeather: return "보우마스터 직업군 착용 가능";
                case GearType.crossBowThimble: return "신궁 직업군 착용 가능";
                case GearType.shadowerSheath: return "섀도어 직업군 착용 가능";
                case GearType.nightLordPoutch: return "나이트로드 직업군 착용 가능";

                case GearType.viperWristband: return "바이퍼 직업군 착용 가능";
                case GearType.captainSight: return "캡틴 직업군 착용 가능";
                case GearType.connonGunPowder: 
                case GearType.connonGunPowder2: return "캐논 슈터 직업군 착용 가능";
                case GearType.aranPendulum: return "아란 직업군 착용 가능";
                case GearType.evanPaper: return "에반 직업군 착용 가능";
                case GearType.battlemageBall: return "배틀메이지 직업군 착용 가능";
                case GearType.wildHunterArrowHead: return "와일드헌터 직업군 착용 가능";
                case GearType.cygnusGem: return "시그너스 기사단 착용 가능";
                case GearType.powerSource:
                case GearType.energySword: return "제논 착용 가능";
                case GearType.desperado: return "데몬 어벤져 착용 가능";
                case GearType.swordZB:
                case GearType.swordZL: return "제로 착용 가능";
                case GearType.whistle:
                case GearType.magicStick: return "林之灵可以装备";

                case GearType.foxPearl: return "은월 착용 가능";
                case GearType.boxingClaw: return "龙的传人可以装备";

                case GearType.katana:
                case GearType.katana2: return "剑豪可以装备";
                case GearType.fan: return "阴阳师可以装备";

                case GearType.espLimiter:
                case GearType.chess: return "키네시스 착용 가능";

                case GearType.GauntletBuster:
                case GearType.ExplosivePill: return "블래스터 착용 가능";

                default: return null;
            }
        }

        /// <summary>
        /// 获取装备额外职业要求说明的字符串。
        /// </summary>
        /// <param Name="specJob">表示装备属性的reqSpecJob的值。</param>
        /// <returns></returns>
        public static string GetExtraJobReqString(int specJob)
        {
            switch (specJob)
            {
                case 61: return "狂龙战士可以装备";
                case 65: return "爆莉萌天使可以装备";
                case 36: return "尖兵可以装备";
                default: return null;
            }
        }

        public static string GetItemPropString(ItemPropType propType, int value)
        {
            switch (propType)
            {
                case ItemPropType.tradeBlock:
                    return GetGearPropString(GearPropType.tradeBlock, value);
                case ItemPropType.tradeAvailable:
                    return GetGearPropString(GearPropType.tradeAvailable, value);
                case ItemPropType.only:
                    return GetGearPropString(GearPropType.only, value);
                case ItemPropType.accountSharable:
                    return GetGearPropString(GearPropType.accountSharable, value);
                case ItemPropType.quest:
                    return value == 0 ? null : "任务道具";
                case ItemPropType.pquest:
                    return value == 0 ? null : "组队任务道具";
                default:
                    return null;
            }
        }

        public static string GetSkillReqAmount(int skillID, int reqAmount)
        {
            switch (skillID / 10000)
            {
                case 11200: return "[需要巨熊技能点: " + reqAmount + "]";
                case 11210: return "[需要雪豹技能点: " + reqAmount + "]";
                case 11211: return "[需要猛禽技能点: " + reqAmount + "]";
                case 11212: return "[需要猫咪技能点: " + reqAmount + "]";
                default: return "[需要？？技能点: " + reqAmount + "]";
            }
        }

        public static string GetJobName(int jobCode)
        {
            switch (jobCode)
            {
                case 0: return "新手";
                case 100: return "战士";
                case 110: return "剑客";
                case 111: return "勇士";
                case 112: return "英雄";
                case 120: return "准骑士";
                case 121: return "骑士";
                case 122: return "圣骑士";
                case 130: return "枪战士";
                case 131: return "龙骑士";
                case 132: return "黑骑士";
                case 200: return "魔法师";
                case 210: return "法师（火，毒）";
                case 211: return "巫师（火，毒）";
                case 212: return "魔导师（火，毒）";
                case 220: return "法师（冰，雷）";
                case 221: return "巫师（冰，雷）";
                case 222: return "魔导师（冰，雷）";
                case 230: return "牧师";
                case 231: return "祭司";
                case 232: return "主教";
                case 300: return "弓箭手";
                case 310: return "猎人";
                case 311: return "射手";
                case 312: return "神射手";
                case 320: return "弩弓手";
                case 321: return "游侠";
                case 322: return "箭神";
                case 400: return "飞侠";
                case 410: return "刺客";
                case 411: return "无影人";
                case 412: return "隐士";
                case 420: return "侠客";
                case 421: return "独行客";
                case 422: return "侠盗";
                case 430: return "见习刀客";
                case 431: return "双刀客";
                case 432: return "双刀侠";
                case 433: return "血刀";
                case 434: return "暗影双刀";
                case 500: return "海盗";
                case 501: return "海盗(炮手)";
                case 510: return "拳手";
                case 511: return "斗士";
                case 512: return "冲锋队长";
                case 520: return "火枪手";
                case 521: return "大副";
                case 522: return "船长";
                case 530: return "火炮手";
                case 531: return "毁灭炮手";
                case 532: return "神炮王";

                case 1000: return "初心者";
                case 1100:
                case 1110:
                case 1111:
                case 1112: return "魂骑士";
                case 1200:
                case 1210:
                case 1211:
                case 1212: return "炎术士";
                case 1300:
                case 1310:
                case 1311:
                case 1312: return "风灵使者";
                case 1400:
                case 1410:
                case 1411:
                case 1412: return "夜行者";
                case 1500:
                case 1510:
                case 1511:
                case 1512: return "奇袭者";

                case 2000: return "战童";
                case 2001: return "小不点";
                case 2002: return "双弩精灵";
                case 2100: return "战神(1次)";
                case 2110: return "战神(2次)";
                case 2111: return "战神(3次)";
                case 2112: return "战神(4次)";
                case 2200: return "龙神(1次)";
                case 2210: return "龙神(2次)";
                case 2211: return "龙神(3次)";
                case 2212: return "龙神(4次)";
                case 2213: return "龙神(5次)";
                case 2214: return "龙神(6次)";
                case 2215: return "龙神(7次)";
                case 2216: return "龙神(8次)";
                case 2217: return "龙神(9次)";
                case 2218: return "龙神(10次)";
                case 2300: return "双弩精灵(1次)";
                case 2310: return "双弩精灵(2次)";
                case 2311: return "双弩精灵(3次)";
                case 2312: return "双弩精灵(4次)";
                case 2400: return "幻影(1次)";
                case 2410: return "幻影(2次)";
                case 2411: return "幻影(3次)";
                case 2412: return "幻影(4次)";
                case 2700: return "夜光(1次)";
                case 2710: return "夜光(2次)";
                case 2711: return "夜光(3次)";
                case 2712: return "夜光(4次)";


                case 3000: return "预备兵";
                case 3001:
                case 3100: return "恶魔猎手(1次)";
                case 3110: return "恶魔猎手(2次)";
                case 3111: return "恶魔猎手(3次)";
                case 3112: return "恶魔猎手(4次)";
                case 3101: return "恶魔复仇者(1次)";
                case 3120: return "恶魔复仇者(2次)";
                case 3121: return "恶魔复仇者(3次)";
                case 3122: return "恶魔复仇者(4次)";
                case 3200: return "唤灵斗师(1次)";
                case 3210: return "唤灵斗师(2次)";
                case 3211: return "唤灵斗师(3次)";
                case 3212: return "唤灵斗师(4次)";
                case 3300: return "豹弩游侠(1次)";
                case 3310: return "豹弩游侠(2次)";
                case 3311: return "豹弩游侠(3次)";
                case 3312: return "豹弩游侠(4次)";
                case 3500: return "机械师(1次)";
                case 3510: return "机械师(2次)";
                case 3511: return "机械师(3次)";
                case 3512: return "机械师(4次)";
                case 3002: return "尖兵";
                case 3600: return "尖兵(1次)";
                case 3610: return "尖兵(2次)";
                case 3611: return "尖兵(3次)";
                case 3612: return "尖兵(4次)";

                case 4001: return "剑豪";
                case 4002: return "阴阳师";
                case 4100: return "剑豪(1次)";
                case 4110: return "剑豪(2次)";
                case 4111: return "剑豪(3次)";
                case 4112: return "剑豪(4次)";
                case 4200: return "阴阳师(1次)";
                case 4210: return "阴阳师(2次)";
                case 4211: return "阴阳师(3次)";
                case 4212: return "阴阳师(4次)";


                case 5000: return "无名少年";
                case 5100: return "米哈尔(1次)";
                case 5110: return "米哈尔(2次)";
                case 5111: return "米哈尔(3次)";
                case 5112: return "米哈尔(4次)";


                case 6000: return "狂龙战士";
                case 6100: return "狂龙战士(1次)";
                case 6110: return "狂龙战士(2次)";
                case 6111: return "狂龙战士(3次)";
                case 6112: return "狂龙战士(4次)";
                case 6001: return "爆莉萌天使";
                case 6500: return "爆莉萌天使(1次)";
                case 6510: return "爆莉萌天使(2次)";
                case 6511: return "爆莉萌天使(3次)";
                case 6512: return "爆莉萌天使(4次)";

                case 10000: return "神之子";
                case 10100: return "神之子(1次)";
                case 10110: return "神之子(2次)";
                case 10111: return "神之子(3次)";
                case 10112: return "神之子(4次)";

                case 11000: return "林之灵";
                case 11200: return "林之灵(1次)";
                case 11210: return "林之灵(2次)";
                case 11211: return "林之灵(3次)";
                case 11212: return "林之灵(4次)";

                case 14000: return "超能力者";
                case 14200: return "超能力者(1次)";
                case 14210: return "超能力者(2次)";
                case 14211: return "超能力者(3次)";
                case 14212: return "超能力者(4次)";
            }
            return null;
        }
    }
}
