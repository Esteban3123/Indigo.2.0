CREATE TABLE [Payroll].[Company] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit]                 VARCHAR (50) NULL,
    [ThirdPartyId]        INT          NULL,
    [Name]                VARCHAR (50) NOT NULL,
    [LegalRepresentative] VARCHAR (50) NOT NULL,
    [CityId]              INT          NOT NULL,
    [PayrollType]         BIT          NULL,
    [AgreementType]       BIT          NULL,
    [State]               BIT          NOT NULL,
    [CreationUser]        VARCHAR (20) NOT NULL,
    [CreationDate]        DATETIME     NOT NULL,
    [ModificationUser]    VARCHAR (20) NULL,
    [ModificationDate]    DATETIME     NULL,
    [TimeStamp]           ROWVERSION   NOT NULL,
    [ARLFundId]           INT          NULL,
    [ForeclousureType]    BIT          NULL,
    [CodeJudgmentAccount] VARCHAR (50) NULL,
    CONSTRAINT [PK_Company__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Company_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Company_Fund] FOREIGN KEY ([ARLFundId]) REFERENCES [Payroll].[Fund] ([Id]),
    CONSTRAINT [FK_Company_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payroll].[Company] NOCHECK CONSTRAINT [FK_Company_City];


GO
ALTER TABLE [Payroll].[Company] NOCHECK CONSTRAINT [FK_Company_Fund];


GO
ALTER TABLE [Payroll].[Company] NOCHECK CONSTRAINT [FK_Company_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_Company__ThirdPartyId]
    ON [Payroll].[Company]([ThirdPartyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si la Empresa es de Tipo Juzgado (ForeclousureType = 1), acá se coloca el código del juzgado para el Archivo Plano del Banco Agrario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'CodeJudgmentAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Embargos (Para los juzgados)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'ForeclousureType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Fondo de ARL para el archivo de Autoliquidacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'ARLFundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la empresa 1-Activo 0- Inactivo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Empresa Tipo: Convenios (1 - Activo, 0 - Inactivo). Se marca para que esta empresa se muestre en el formulario de Convenios. Si no está marcado, allá no se encontrará', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'AgreementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si la Empresa es para Liquidar Nómina: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'PayrollType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fk Id de la ciudad donde esta la empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'CityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Representante legal', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'LegalRepresentative';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Tercero (ThirdParty)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit de la Empresa', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id tabla de empresas', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Company', @level2type = N'COLUMN', @level2name = N'Id';

