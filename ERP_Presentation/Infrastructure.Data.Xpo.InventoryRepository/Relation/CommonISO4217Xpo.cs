using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using Infrastructure.Data.Xpo.InventoryRepository;

[Persistent("Common.ISO4217")]
public partial class ISO4217Xpo : XPLiteObject
{
    public ISO4217Xpo(Session session) : base(session) { }
    public ISO4217Xpo() : this(Session.DefaultSession) { }
    public override void AfterConstruction() { base.AfterConstruction(); }

    int fId;
    [Key(true)]
    public int Id
    {
        get { return fId; }
        set { SetPropertyValue<int>("Id", ref fId, value); }
    }

    string fCodeAbbreviation;
    [Persistent("CodeAbbreviation")]
    public string CodeAbbreviation
    {
        get { return fCodeAbbreviation; }
        set { SetPropertyValue<string>("CodeAbbreviation", ref fCodeAbbreviation, value); }
    }

    string fCurrencyName;
    [Persistent("CurrencyName")]
    public string CurrencyName
    {
        get { return fCurrencyName; }
        set { SetPropertyValue<string>("CurrencyName", ref fCurrencyName, value); }
    }

    [PersistentAlias("concat(CodeAbbreviation,' - ', CurrencyName)")]
    public string CodeName
    {
        get { return Convert.ToString(EvaluateAlias("CodeName")); }
    }

    [Association("ISO4217Xpo_Reference_Currency", typeof(CurrencyXpo))]
    public XPCollection<CurrencyXpo> CurrencyXpo
    {
        get { return GetCollection<CurrencyXpo>("CurrencyXpo"); }
    }
}