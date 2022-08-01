using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_RPG
{
    class Player : GameCharacter
    {
        public byte STR { get; set; }
        static Random r = new Random();

        public Player()
        {
            HP = byte.MaxValue;
            ATK = 10;
            STR = 5;
        }

       public void Attack(Enemy enemy, Elements currentSpell, byte enemyPosX, byte enemyPosY) => 
            enemy.GetDamage(CalculateDamage(enemy, currentSpell), enemyPosX, enemyPosY);

        private byte CalculateDamage(Enemy enemy, Elements currentSpell)
        {
            float maxAttRoll = 8 * ((float)ATK / 64);
            maxAttRoll += Program.Compare(currentSpell, enemy.Element) == 1 ? maxAttRoll * .2f : 0; 
            float maxDefRoll = 8 * ((float)enemy.DEF / 64);
            float accuracy;
            byte damage;

            if (ATK < enemy.DEF)
                accuracy = 0.5f * maxAttRoll / maxDefRoll;

            else 
                accuracy = 1 - 0.5f * maxDefRoll / maxAttRoll;

            if (r.NextDouble() < accuracy)
            {
                damage = (byte)r.Next(1, STR + 1);

                // Calcul du bonus - ou malus - obtenu par l'incantation du sortilège
                if (currentSpell != Elements.None)
                {
                    switch (Program.Compare(currentSpell, enemy.Element))
                    {
                        case 1:
                            damage += (byte)(damage * .2f);
                            Program.Print($"{currentSpell}-type attack doubled: {damage} on {enemy.Element}-type foe!");
                            break;

                        case -1:
                            damage >>= 1;
                            Program.Print($"{currentSpell}-type attack halved: {damage} on {enemy.Element}-type foe!");
                            break;

                        default:
                            Program.Print($"{currentSpell}-type attack unchanged: {damage} on {enemy.Element}-type foe!");
                            break;
                    }
                }

                else
                    Program.Print($"Normal attack launched: {damage} on foe!");

                if (damage > enemy.HP)
                    damage = enemy.HP;
            }

            else
            {
                damage = 0;
                Program.Print("No damage this turn!");
            }

            return damage;
        }
    }
}
