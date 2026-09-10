CREATE TABLE [Contract].[Groupers] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)    NOT NULL,
    [Description]          VARCHAR (100)   NOT NULL,
    [ParentId]             INT             NULL,
    [UserNumber]           INT             CONSTRAINT [DF_Groupers_UserNumber] DEFAULT ((0)) NOT NULL,
    [UserMin]              INT             CONSTRAINT [DF_Groupers_UserMin] DEFAULT ((0)) NOT NULL,
    [UserMax]              INT             CONSTRAINT [DF_Groupers_UserMax] DEFAULT ((0)) NOT NULL,
    [ProjectCME]           NUMERIC (18)    CONSTRAINT [DF_Groupers_ProjectCME] DEFAULT ((0)) NOT NULL,
    [Frequence]            NUMERIC (18, 8) CONSTRAINT [DF_Groupers_Frequence] DEFAULT ((0)) NOT NULL,
    [TotalContract]        NUMERIC (18)    CONSTRAINT [DF_Groupers_TotalContract] DEFAULT ((0)) NOT NULL,
    [UserValue]            NUMERIC (18)    CONSTRAINT [DF_Groupers_UserValue] DEFAULT ((0)) NOT NULL,
    [MinimunRange]         INT             CONSTRAINT [DF_Groupers_MinimunRange] DEFAULT ((0)) NOT NULL,
    [MaximunRange]         INT             CONSTRAINT [DF_Groupers_MaximunRange] DEFAULT ((0)) NOT NULL,
    [MeasurementUnit]      TINYINT         CONSTRAINT [DF_Groupers_MeasurementUnit] DEFAULT ((0)) NOT NULL,
    [WarningFor]           INT             CONSTRAINT [DF_Groupers_WarningFor] DEFAULT ((0)) NOT NULL,
    [WarningMessage]       VARCHAR (500)   NULL,
    [MaximunRangeRestrict] BIT             CONSTRAINT [DF_Groupers_MaximunRangeRestrict] DEFAULT ((0)) NOT NULL,
    [RestrictMessage]      VARCHAR (500)   NULL,
    [Status]               BIT             NOT NULL,
    [CreationUser]         VARCHAR (20)    NOT NULL,
    [CreationDate]         DATETIME        NOT NULL,
    [ModificationUser]     VARCHAR (20)    NULL,
    [ModificationDate]     DATETIME        NULL,
    [TimeStamp]            ROWVERSION      NOT NULL,
    [Version]              INT             DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Groupers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Groupers_Groupers] FOREIGN KEY ([ParentId]) REFERENCES [Contract].[Groupers] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal (TIMESTAMP) del evento de creación, registro o modificación del agrupador de contrato. Captura el instante exacto de cambio en la BD.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del agrupador. Rastreo de cambios en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación del agrupador de contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro del agrupador en el contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro del agrupador de contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT): 1=Activo, 0=Inactivo. Controla si el agrupador está vigente en contratos.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado: 1 - Activo, 0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de restricción (VARCHAR 500) mostrado cuando se alcanza o supera el rango máximo permitido.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'RestrictMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje de Restricción', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'RestrictMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'RestrictMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que activa/desactiva la restricción de rango máximo en el agrupador de contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MaximunRangeRestrict';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Restringir Rango Máximo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MaximunRangeRestrict';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MaximunRangeRestrict';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de advertencia (VARCHAR 500) que se muestra al aproximarse al umbral de cantidad/valor establecido.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'WarningMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje de Advertencia', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'WarningMessage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'WarningMessage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Umbral (INT) a partir del cual se dispara advertencia. Usado en monitoreo de uso de servicios en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'WarningFor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Advertir a partir de', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'WarningFor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'WarningFor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida (TINYINT): tipo de escala para Frecuencia, Rango, Usuario. Ej: unidades, porcentaje, cantidad.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MeasurementUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rango máximo (INT) permitido para el agrupador. Límite superior de cantidad o cobertura en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MaximunRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango Máximo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MaximunRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MaximunRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rango mínimo (INT) requerido para el agrupador. Límite inferior de cantidad o cobertura en contrato.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MinimunRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rango Minimo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MinimunRange';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'MinimunRange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario (NUMERIC 18) asignado por usuario. Costo o precio individual del agrupador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del usuario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total (NUMERIC 18) del contrato asociado al agrupador. Monto global facturado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'TotalContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contrato total', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'TotalContract';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'TotalContract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia (NUMERIC 18,8) de ocurrencia del agrupador. Tasa, proporción o repetición en el período contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Frequence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Frequence';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Frequence';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Proyecto CME (NUMERIC 18): presupuesto o meta de Educación Médica Continua asociada al agrupador.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ProjectCME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proyecto CME', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ProjectCME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ProjectCME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Máximo número (INT) de usuarios permitidos en el agrupador de contrato. Límite superior de cobertura.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserMax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Maximo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserMax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserMax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mínimo número (INT) de usuarios requeridos en el agrupador de contrato. Límite inferior de cobertura.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserMin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Minimo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserMin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserMin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad (INT) de usuarios asignados al agrupador. Número actual de beneficiarios o afiliados.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de usuario', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'UserNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del agrupador padre. Referencia jerárquica a agrupador superior (autorrelacionado).', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ParentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del padre', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ParentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'ParentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual (VARCHAR 100) del agrupador de contrato. Nombre o denominación completa.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) del agrupador de contrato. Identificador corto para búsqueda y referencia rápida.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT, PK, IDENTITY) del agrupador de contrato. Clave primaria secuencial.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agrupadores de contrato: define la estructura jerárquica de grupos y subgrupos utilizados en contratos, con sus límites de usuarios, rangos de cantidad, frecuencias, valores proyectados y mensajes de advertencia o restricción para el control de la contratación.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de versión del registro del agrupador, usado para control de concurrencia y auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Version';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Groupers', @level2type = N'COLUMN', @level2name = N'Version';
