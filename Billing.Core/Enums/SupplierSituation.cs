using System.ComponentModel;

namespace Billing.Core.Enums
{
    public enum SupplierSituation
    {
        [Description("Em Elaboração")]
        EmElaboracao = 0,

        [Description("Ativado")]
        Ativado = 1,

        [Description("Desativado")]
        Desativado = 2
    }
}
