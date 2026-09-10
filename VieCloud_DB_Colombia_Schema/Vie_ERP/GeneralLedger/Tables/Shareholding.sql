CREATE TABLE [GeneralLedger].[Shareholding] (
    [Id]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)   NOT NULL,
    [Name]             VARCHAR (100)  NULL,
    [Part]             DECIMAL (5, 2) NOT NULL,
    [Status]           BIT            NOT NULL,
    [CreationUser]     VARCHAR (20)   NOT NULL,
    [CreationDate]     DATETIME       NOT NULL,
    [ModificationUser] VARCHAR (20)   NULL,
    [ModificationDate] DATETIME       NULL,
    [TimeStamp]        ROWVERSION     NOT NULL,
    CONSTRAINT [PK_PatrimonialPart] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP SQL Server). Registra automáticamente el instante exacto de creación, modificación o cambio de estado del registro de accionista. Uso: control de versiones y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro de accionista. DATETIME. Nulo si nunca fue modificado tras creación. Búsqueda: cambios recientes, auditoría.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que realizó la última modificación del registro (VARCHAR 20). Nulo si no hay cambios post-creación. Trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de accionista. DATETIME obligatorio. Marca el inicio del registro en el sistema contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que creó el registro de accionista (VARCHAR 20). Requerido. Auditoría inicial de quién registró la entidad accionaria.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del accionista. BIT: 1=Activo (participa en distribución), 0=Inactivo (historial, sin participación actual). Control operacional.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro True = Activo | False = Inactivo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de participación accionaria o patrimonial. DECIMAL(5,2). Rango: 0.00 a 100.00%. Suma total de todos los accionistas = 100%. Cálculo de distribuciones.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Part';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de participación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Part';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Part';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o razón social de la entidad accionista. VARCHAR(100). Búsqueda: nombre del socio, inversor, propietario, empresa matriz o filial.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la entidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único identificador del accionista (VARCHAR 20). No duplicable. Clave para búsqueda rápida y referencia en módulos de distribución de utilidades.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la entidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico secuencial único del registro de accionista (INT IDENTITY). Clave primaria. Referencia en relaciones de distribución patrimonial y utilidades.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Participaciones accionarias o socios de la organización: registra cada accionista o partícipe con su código, nombre, porcentaje de participación y estado activo/inactivo. Se usa en contabilidad general para distribuir resultados, utilidades o costos según la proporción de cada socio.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'Shareholding';
