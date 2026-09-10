CREATE TABLE [Payroll].[Fund] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)  NOT NULL,
    [ThirdPartyId]            INT           NOT NULL,
    [Name]                    VARCHAR (100) NOT NULL,
    [Unemployment]            BIT           NOT NULL,
    [Health]                  BIT           NOT NULL,
    [Pension]                 BIT           NOT NULL,
    [Risk]                    BIT           NOT NULL,
    [CompensationFund]        BIT           NOT NULL,
    [Type]                    TINYINT       NOT NULL,
    [MinistryCode]            VARCHAR (6)   NOT NULL,
    [State]                   BIT           NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    [Term]                    INT           NULL,
    [MainAccountReceivableId] INT           NULL,
    [PensionACCAI]            BIT           DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Fund__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Fund_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [UQ_Fund__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_Fund__Code__State]
    ON [Payroll].[Fund]([Code] ASC, [State] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Fund__ThirdPartyId]
    ON [Payroll].[Fund]([ThirdPartyId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de afiliación a fondo de pensión ACCAI conforme Ley 2381 de 2024; tipo BIT (0=No, 1=Sí); aplica a fondos de pensiones complementarias.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'PensionACCAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pension ACCAI segun  Ley_2381_de_2024', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'PensionACCAI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'PensionACCAI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK de cuenta por cobrar (Accounts Receivable) asociada al fondo; usado en documentos de facturación y cartera; INT.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'MainAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar del cliente, este campo se usa cuando se hace un documento de Cuenta por Cobrar', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'MainAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'MainAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plazo en días para vencimiento o condición de pago del fondo; tipo INT; relevante en negociaciones contractuales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plazo (Días)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Term';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Term';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (timestamp) de evento de auditoría; registra automáticamente creación, modificación o cambio de estado del fondo en BD.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro del fondo; tipo DATETIME; nulo si nunca fue editado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o login del usuario que realizó última modificación del fondo; tipo VARCHAR(20); nulo si registro no fue editado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación inicial del registro del fondo en sistema; tipo DATETIME; auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o login del usuario que creó el registro del fondo; tipo VARCHAR(20); auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo del fondo (1=Activo, 0=Inactivo); tipo BIT; indica si fondo está habilitado para afiliación y cotizaciones.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del fondo  1- Activo 0 - Inactivo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del fondo asignado por Ministerio de Salud (MinSalud); tipo VARCHAR(6); identificador regulatorio nacional.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'MinistryCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de MiniSalud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'MinistryCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'MinistryCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ámbito institucional del fondo (0=Público, 1=Privado); tipo TINYINT; determina régimen y normativa aplicable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de ambito del fondo 0-Publico 1- Privado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de caja de compensación familiar; tipo BIT (0=No, 1=Sí); beneficio obligatorio en Colombia.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CompensationFund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caja de compensacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CompensationFund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'CompensationFund';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cobertura de seguro de riesgo laboral (ARL); tipo BIT (0=No, 1=Sí); protección contra accidentes y enfermedades ocupacionales.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Risk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Riesgo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Risk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Risk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de fondo de pensiones obligatorio; tipo BIT (0=No, 1=Sí); sistema de jubilación y pensión del afiliado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Pension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pension', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Pension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Pension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de afiliación a sistema de salud (EPS); tipo BIT (0=No, 1=Sí); cobertura de servicios médicos y hospitalarios.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Health';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salud', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Health';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Health';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de fondo de cesantía (desempleo); tipo BIT (0=No, 1=Sí); protección ante terminación de contrato laboral.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Unemployment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cesantias', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Unemployment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Unemployment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social del fondo; tipo VARCHAR(100); identificación legible de la entidad prestadora.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del fondo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK del tercero (entidad, empresa o persona jurídica) propietaria del fondo; referencia a [Common].[ThirdParty].', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del fondo; tipo VARCHAR(20); identificador alfanumérico exclusivo para búsqueda y referencia en sistemas.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Fondo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador primario (PK) autonumérico del fondo; tipo INT IDENTITY; clave única para cada registro en tabla.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico para los fondos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fondos y entidades administradoras de seguridad social y parafiscales (EPS, AFP, ARL, cajas de compensación familiar, cesantías) a los que se realizan aportes en nómina. Registra el tipo de fondo, los conceptos que administra (salud, pensión, riesgo laboral, etc.) y su estado de vigencia.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Fund';
