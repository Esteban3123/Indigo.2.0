using DevExpress.Xpo;
using Infrastructure.Data.Xpo.InventoryRepository;
using System;

[Persistent("MixingStation.ContractExternalClients")]
public class ContractExternalClientsXpo : XPLiteObject
{
    private int fId;

    [Key(true)]
    [Persistent("Id")]
    public int Id
    {
        get
        {
            return fId;
        }
        set
        {
            SetPropertyValue<int>("Id", ref fId, value);
        }
    }

    private string fCode;

    [Persistent("Code")]
    public string Code
    {
        get
        {
            return fCode;
        }
        set
        {
            SetPropertyValue<string>("Code",ref fCode, value);
        }
    }

    private string fContractNumber;

    [Persistent("ContractNumber")]
    public string ContractNumber
    {
        get
        {
            return fContractNumber;
        }
        set
        {
            SetPropertyValue<string>("ContractNumber", ref fContractNumber, value);
        }
    } 

    [PersistentAlias("concat(Code,' - ',ContractNumber)")]
    public string CodeNumber
    {
        get
        {
            return Convert.ToString(this.EvaluateAlias("CodeNumber"));
        }
    }

    [Association("DocumentInvoiceProductSalesReferencesContractExternalClients", typeof(InventoryDocumentInvoiceProductSalesXpo))]
    public XPCollection<InventoryDocumentInvoiceProductSalesXpo> InventoryDocumentInvoiceProductSalesXpo
    {
        get
        {
            return GetCollection<InventoryDocumentInvoiceProductSalesXpo>("InventoryDocumentInvoiceProductSalesXpo");
        }
    }

    public ContractExternalClientsXpo(Session session) : base(session)
    {
    }

    public ContractExternalClientsXpo() : base(Session.DefaultSession)
    {
    }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
    }
}