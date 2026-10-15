CREATE TABLE [Glosas].[GlosasParametersInterface] (
    [Id]                                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContainerName]                        VARCHAR (50)  NOT NULL,
    [CompanyName]                          VARCHAR (100) NOT NULL,
    [Interface]                            BIT           NOT NULL,
    [AccountingMethod]                     INT           NOT NULL,
    [AffectsService]                       BIT           NOT NULL,
    [AccountantAccountGeneralAcceptanceC]  VARCHAR (15)  NULL,
    [AccountantAccountPreviousAcceptanceC] VARCHAR (15)  NULL,
    [DebitAccount]                         VARCHAR (15)  NULL,
    [AcceptanceObjectionsConcept]          VARCHAR (15)  NULL,
    [AcceptanceObjectionsConceptPast]      VARCHAR (15)  NULL,
    [AcceptancReiterationsConcept]         VARCHAR (15)  NULL,
    [DateConfiguration]                    DATETIME      CONSTRAINT [DF_GlosasParametersInterface_DateConfiguration] DEFAULT ([Common].[getdate]()) NOT NULL,
    [RadicateCodeNoteAccounting]           VARCHAR (10)  NULL,
    [DevolutionCodeNoteAccounting]         VARCHAR (10)  NULL,
    [DevolutionInjustificate]              INT           NULL,
    [CreationUser]                         VARCHAR (20)  CONSTRAINT [DF_GlosasParametersInterface_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]                         DATETIME      CONSTRAINT [DF_GlosasParametersInterface_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]                     VARCHAR (20)  NULL,
    [ModificationDate]                     DATETIME      NULL,
    [TimeStamp]                            ROWVERSION    NOT NULL,
    CONSTRAINT [PK_GlosasParametersInterface__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1) liberar factura del radicado  2) mantener factura en el radicado  3) definido por el usuario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DevolutionInjustificate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del tipo de documento para la Nota Contable de Devolucion de facturas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DevolutionCodeNoteAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del tipo de documento para la Nota Contable de Radicacion Cuentas de Cobro', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'RadicateCodeNoteAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion de la configuración', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DateConfiguration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto de Aceptacion De Reiteraciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptancReiterationsConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto para aceptaciones de vigencias anteriores', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConceptPast';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto para las aceptaciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Debito de Orden para el sector Publico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DebitAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta general de aceptaciones de años anteriores', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountantAccountPreviousAcceptanceC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta general de Aceptaciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountantAccountGeneralAcceptanceC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta Servicio 0=No 1=Si', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AffectsService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Metodo contable      1=Interfaz Fox - Metodo Privado        2=Interfaz Fox - Metodo Publico           3= Interfaz NET - Metodo Privado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountingMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Realiza Interfaz 0=No 1=Si', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'Interface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre De la Empresa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CompanyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Alias de la Base de datos a realizar interfaz', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ContainerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de parametros de interfacez', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';

