CREATE TABLE [Common].[CompanyIndigo] (
    [Id]                  TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [VerificationNumber]  CHAR (1)      NULL,
    [CompanyCode]         CHAR (2)      NOT NULL,
    [CompanyName]         VARCHAR (90)  NOT NULL,
    [City]                VARCHAR (50)  NULL,
    [RepresentationLegal] VARCHAR (50)  NULL,
    [Telephone]           VARCHAR (100) NULL,
    [Address]             VARCHAR (100) NULL,
    [KeyCode]             CHAR (20)     NOT NULL,
    [CompanyNit]          CHAR (15)     NOT NULL,
    [IdentificationType]  CHAR (1)      NULL,
    [Identification]      CHAR (10)     NULL,
    [ProductionCompany]   BIT           NOT NULL,
    [State]               BIT           NOT NULL,
    [Synchronized]        CHAR (1)      NOT NULL,
    [LyncIntegration]     BIT           NULL,
    CONSTRAINT [PK_CompanyIndigo__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_CompanyIndigo__State]
    ON [Common].[CompanyIndigo]([State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Integracion Lync', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'LyncIntegration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Estado ni Sincronizado 2- Estado no Sincronizado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Eliminado para Sincronizacion de DB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si la empresa es de produccion = True, o ambiente de pruebas = False', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'ProductionCompany';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Identificacion del Prestador', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Identification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Identificacion del Prestador  1: CC  2: Nit', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit de la empresa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyNit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Serial Generado para la empresa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'KeyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Telephone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Representación Legal', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'RepresentationLegal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ciudad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'City';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la empresa indigo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Empresa Indigo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'CompanyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Verificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'VerificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'CompanyIndigo', @level2type = N'COLUMN', @level2name = N'Id';

