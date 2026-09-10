using DevExpress.Xpo;
using Infrastructure.Data.Xpo.InventoryRepository;

/// <summary>
/// ''' conceptos de nota usado en los servicios Xpo
/// ''' </summary>
using System;

[Persistent("Contract.ContractDescriptions")]
public class ContractDescriptionsXpo : XPLiteObject
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

    private string fName;

    [Persistent("Name")]
    public string Name
    {
        get
        {
            return fName;
        }
        set
        {
            SetPropertyValue<string>("Name",ref fName, value);
        }
    }

    private bool fStatus;

    [Persistent("Status")]
    public bool Status
    {
        get
        {
            return fStatus;
        }
        set
        {
            SetPropertyValue<bool>("Status", ref fStatus, value);
        }
    }

    // columna que devuelve el nit y el nombre concatenado
    [PersistentAlias("concat(Code,' - ',Name)")]
    public string CodeName
    {
        get
        {
            return Convert.ToString(this.EvaluateAlias("CodeName"));
        }
    }

    [PersistentAlias("iif(Status, 'Activo', 'Inactivo')")]
    public string StatusName
    {
        get
        {
            return Convert.ToString(this.EvaluateAlias("StatusName"));
        }
    }

    private bool fSelectOption;

    [NonPersistent()]
    public bool SelectOption
    {
        get
        {
            return fSelectOption;
        }
        set
        {
            fSelectOption = value;
        }
    }


    [Association("ProductRateDetailReferencesContractDescription", typeof(ProductRateDetailXpo))]
    public XPCollection<ProductRateDetailXpo> ProductRateDetail
    {
        get
        {
            return GetCollection<ProductRateDetailXpo>("ProductRateDetail");
        }
    }

    public ContractDescriptionsXpo(Session session) : base(session)
    {
    }

    public ContractDescriptionsXpo() : base(Session.DefaultSession)
    {
    }

    public override void AfterConstruction()
    {
        base.AfterConstruction();
    }
}