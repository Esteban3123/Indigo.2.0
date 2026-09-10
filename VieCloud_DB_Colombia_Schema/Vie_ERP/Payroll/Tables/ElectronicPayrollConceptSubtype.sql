

CREATE TABLE [Payroll].[ElectronicPayrollConceptSubtype] (
    [Id]                          INT           IDENTITY (1, 1) NOT NULL,
    [IdElectronicPayrollConcepts] INT           NOT NULL,
    [Name]                        VARCHAR (100) NOT NULL,
    [InternalSubCode]             TINYINT       NOT NULL,
    [State]                       BIT           DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ElectronicPayrollConceptSubtype_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicPayrollConceptSubtype_ElectronicPayrollConcepts] FOREIGN KEY ([IdElectronicPayrollConcepts]) REFERENCES [Payroll].[ElectronicPayrollConcepts] ([Id]) ON UPDATE CASCADE
);



GO



GO





GO


GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtipos de conceptos de nómina electrónica. Clasifica en categorías más específicas cada concepto de nómina electrónica (como variantes de devengados o deducciones) para el reporte de nómina electrónica ante la DIAN.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del subtipo de concepto de nómina electrónica.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de nómina electrónica al que pertenece este subtipo; referencia el concepto padre (devengado, deducción, etc.).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype', @level2type = N'COLUMN', @level2name = N'IdElectronicPayrollConcepts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype', @level2type = N'COLUMN', @level2name = N'IdElectronicPayrollConcepts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del subtipo de concepto, por ejemplo ''''Horas extras diurnas'''' o ''''Retención en la fuente''''.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo interno del subtipo que se envia en el XML DIAN para discriminar variaciones dentro del mismo Type (InternalCode de ElectronicPayrollConcepts). Valores por Type:

Type 14 - Primas:
  0 = Prima Salarial     → XML: result.Pago
  1 = Prima No Salarial  → XML: result.PagoNS

Type 17 - Incapacidades:
  1 = Incapacidad Comun       → XML: Tipo=1
  2 = Incapacidad Profesional → XML: Tipo=2
  3 = Incapacidad Laboral     → XML: Tipo=3

Type 26 - Otros Conceptos Devengados:
  0 = Salarial    → XML: ConceptoS
  1 = No Salarial → XML: ConceptoNS

Type 29 - Bono EPCTV:
  0 = Bono EPCTV Salarial           → XML: PagoS
  1 = Bono EPCTV No Salarial        → XML: PagoNS
  2 = Bono Alimentacion Salarial    → XML: PagoAlimentacionS
  3 = Bono Alimentacion No Salarial → XML: PagoAlimentacionNS

Type 44 - Sanciones:
  0 = Sancion Publica  → XML: SancionPublic
  1 = Sancion Privada  → XML: SancionPriv

Fuente: NominaIndividual.vb en Domain.ElectronicDocuments.Service\DIAN\UBL2_1\v1.0', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype', @level2type = N'COLUMN', @level2name = N'InternalSubCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'manual_oastudillo@indigo.tech_2026-06-12', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype', @level2type = N'COLUMN', @level2name = N'InternalSubCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el subtipo está activo (1) o inactivo (0); permite habilitar o deshabilitar subtipos sin eliminarlos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ElectronicPayrollConceptSubtype', @level2type = N'COLUMN', @level2name = N'State';
