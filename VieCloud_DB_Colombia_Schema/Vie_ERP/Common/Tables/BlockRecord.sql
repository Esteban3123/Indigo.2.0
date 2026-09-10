CREATE TABLE [Common].[BlockRecord] (
    [Id]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdForm]    INT           NOT NULL,
    [IdRecord]  INT           NOT NULL,
    [CodUser]   VARCHAR (250) NOT NULL,
    [NameUser]  VARCHAR (250) NOT NULL,
    [BlockDate] DATE          NOT NULL,
    CONSTRAINT [PK_BlockRecord] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se bloqueó el registro, timestamp del bloqueo, auditoría de cierre temporal del formulario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'BlockDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se bloqueo el registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'BlockDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'BlockDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario profesional de la salud que bloqueó el registro, identificación del operador, profesional responsable', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'NameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de usuario ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'NameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'NameUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario, identificación del profesional de la salud, login o credencial del operador que efectuó el bloqueo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Usuario ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'CodUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro bloqueado, referencia al registro de atención, ingreso o procedimiento que está bloqueado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'IdRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro que se encuentra bloqueado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'IdRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'IdRecord';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formulario o unidad funcional que contiene el registro bloqueado, referencia al documento clínico bloqueado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del formulario en el que se encuentra bloqueado el registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'IdForm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'IdForm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la tabla BlockRecord, clave primaria del registro de bloqueo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de bloqueos de registros en formularios del sistema. Indica qué usuario tiene bloqueado un registro específico en un formulario, evitando ediciones simultáneas.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BlockRecord';
