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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) que registra automáticamente el instante exacto de creación, modificación o evento en la configuración de parámetros de glosas e interfaz contable.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del registro de parámetros de interfaz de glosas. Null si no ha sido modificado.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador (VARCHAR 20) del usuario que realizó la última modificación de los parámetros de interfaz contable de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó el registro de configuración de parámetros de interfaz de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador (VARCHAR 20) del usuario que creó el registro de parámetros de interfaz contable. Por defecto 999.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario de creacion', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (INT) del comportamiento de devolución de facturas radicadas: 1=Liberar factura del radicado, 2=Mantener factura en el radicado, 3=Definido por el usuario.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DevolutionInjustificate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1) liberar factura del radicado  2) mantener factura en el radicado  3) definido por el usuario', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DevolutionInjustificate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DevolutionInjustificate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 10) del tipo de documento contable (Nota Contable) para registrar devoluciones o desradicaciones de facturas en el sistema contable.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DevolutionCodeNoteAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del tipo de documento para la Nota Contable de Devolucion de facturas', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DevolutionCodeNoteAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DevolutionCodeNoteAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código (VARCHAR 10) del tipo de documento contable (Nota Contable) para registrar radicaciones de cuentas de cobro en el sistema contable.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'RadicateCodeNoteAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del tipo de documento para la Nota Contable de Radicacion Cuentas de Cobro', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'RadicateCodeNoteAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'RadicateCodeNoteAccounting';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación o configuración inicial de los parámetros de interfaz de glosas y contabilidad.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DateConfiguration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion de la configuración', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DateConfiguration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DateConfiguration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de concepto contable (VARCHAR 15) para registrar aceptaciones de glosas reiteradas o recursos presentados nuevamente.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptancReiterationsConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de Aceptacion De Reiteraciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptancReiterationsConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptancReiterationsConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de concepto contable (VARCHAR 15) para registrar aceptaciones de objeciones o glosas de vigencias, períodos o años anteriores.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConceptPast';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto para aceptaciones de vigencias anteriores', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConceptPast';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConceptPast';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de concepto contable (VARCHAR 15) para registrar aceptaciones de objeciones, glosas o recursos en el período actual.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto para las aceptaciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AcceptanceObjectionsConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de débito (VARCHAR 15) utilizada para el sector público en órdenes y transacciones de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DebitAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Debito de Orden para el sector Publico', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DebitAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'DebitAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable general (VARCHAR 15) donde se registran aceptaciones de glosas de años o períodos anteriores.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountantAccountPreviousAcceptanceC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta general de aceptaciones de años anteriores', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountantAccountPreviousAcceptanceC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountantAccountPreviousAcceptanceC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable general (VARCHAR 15) donde se registran las aceptaciones de glosas del período actual.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountantAccountGeneralAcceptanceC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta general de Aceptaciones', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountantAccountGeneralAcceptanceC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountantAccountGeneralAcceptanceC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si la configuración afecta la prestación de servicios: 0=No afecta, 1=Sí afecta servicio.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AffectsService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Afecta Servicio 0=No 1=Si', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AffectsService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AffectsService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método contable (INT) de interfaz: 1=Fox Privado, 2=Fox Público, 3=NET Privado. Define cómo se procesan glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountingMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Metodo contable      1=Interfaz Fox - Metodo Privado        2=Interfaz Fox - Metodo Publico           3= Interfaz NET - Metodo Privado', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountingMethod';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'AccountingMethod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si se realiza interfaz contable: 0=No realiza interfaz, 1=Sí realiza interfaz con contabilidad.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'Interface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Realiza Interfaz 0=No 1=Si', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'Interface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'Interface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la empresa o entidad prestadora (VARCHAR 100) para la cual se configura la interfaz de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CompanyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre De la Empresa', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CompanyName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'CompanyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Alias o identificador (VARCHAR 50) de la base de datos destino donde se realiza la interfaz de parámetros de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ContainerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Alias de la Base de datos a realizar interfaz', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ContainerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'ContainerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) que genera automáticamente el registro de parámetros de interfaz de glosas. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de parametros de interfacez', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración del módulo de glosas para la integración contable y operativa. Define cómo se contabilizan las aceptaciones, objeciones y reiteraciones de glosas por empresa, incluyendo las cuentas contables, conceptos y métodos asociados al proceso de gestión de glosas con aseguradoras o pagadores.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'GlosasParametersInterface';
