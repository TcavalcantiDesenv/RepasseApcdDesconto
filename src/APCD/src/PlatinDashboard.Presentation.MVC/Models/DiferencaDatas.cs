using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlatinDashboard.Presentation.MVC.Models
{
    public class DiferencaDatas
    {
  /// <summary>
            /// definindo os números de dias em um mês; index 0=> janeiro e 11=> Dezembro
            /// fevereiro contém ou 28 ou 29 dias, por isso temos o valor -1
            /// o que iremos usar para calcular
            /// </summary>
            private int[] diasDoMes = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            /// <summary>
            /// contém a data inicial
            /// </summary>
            private DateTime dataInicial;

            /// <summary>
            /// contém a data final
            /// </summary>
            private DateTime dataFinal;

            public DiferencaDatas(DateTime d1, DateTime d2)
            {
                int incremento;

                if (d1 > d2)
                {
                    this.dataInicial = d2;
                    this.dataFinal = d1;
                }
                else
                {
                    this.dataInicial = d1;
                    this.dataFinal = d2;
                }

                /// 
                /// Calculo dos dias
                /// 
                incremento = 0;

                if (this.dataInicial.Day > this.dataFinal.Day)
                {
                    incremento = this.diasDoMes[this.dataInicial.Month - 1];

                }
                /// se for fevereiro
                /// se odia for menor que o dia de  hoje
                if (incremento == -1)
                {
                    if (DateTime.IsLeapYear(this.dataInicial.Year))
                    {
                        // ano bissexto -> fevereiro contém 29 dias
                        incremento = 29;
                    }
                    else
                    {
                        incremento = 28;
                    }
                }
                if (incremento != 0)
                {
                    Dias = (this.dataFinal.Day + incremento) - this.dataInicial.Day;
                    incremento = 1;
                }
                else
                {
                    Dias = this.dataFinal.Day - this.dataInicial.Day;
                }

                ///
                ///calculo do mês
                ///
                if ((this.dataInicial.Month + incremento) > this.dataFinal.Month)
                {
                    this.Meses = (this.dataFinal.Month + 12) - (this.dataInicial.Month + incremento);
                    incremento = 1;
                }
                else
                {
                    this.Meses = (this.dataFinal.Month) - (this.dataInicial.Month + incremento);
                    incremento = 0;
                }

                ///
                /// calculo do ano
                ///
                this.Anos = this.dataFinal.Year - (this.dataInicial.Year + incremento);

            }

            public override string ToString()
            {
                return this.Meses.ToString();
            }

            public int Anos { get; }
            public int Meses { get; }
            public int Dias { get; }
        }
    }
