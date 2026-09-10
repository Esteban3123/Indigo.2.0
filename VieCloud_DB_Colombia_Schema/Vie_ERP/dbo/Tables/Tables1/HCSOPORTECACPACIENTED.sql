CREATE TABLE [dbo].[HCSOPORTECACPACIENTED] (
    [ID]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCSOPORTECAC]         INT           NOT NULL,
    [IDHCSOPORTECACPACIENTE] INT           NOT NULL,
    [IPCODPACI]              VARCHAR (25)  NOT NULL,
    [MENSAJE]                VARCHAR (MAX) NOT NULL,
    [FECHACREACION]          DATETIME      NOT NULL,
    CONSTRAINT [PK_HCSOPORTECACPACIENTED] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCSOPORTECACPACIENTED_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCSOPORTECACPACIENTED_SoporteCAC] FOREIGN KEY ([IDHCSOPORTECAC]) REFERENCES [dbo].[HCSOPORTECAC] ([ID]),
    CONSTRAINT [FK_HCSOPORTECACPACIENTED_SoporteCACD] FOREIGN KEY ([IDHCSOPORTECACPACIENTE]) REFERENCES [dbo].[HCSOPORTECACPACIENTE] ([ID])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de detalle de soporte CAC (DATETIME). Marca temporal del evento de error en generación de soporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha Creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de error o descripción capturado por el catch de excepción en la generación de soporte CAC (VARCHAR MAX). Trazabilidad técnica del problema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'MENSAJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje capturado por el cacth ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'MENSAJE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'MENSAJE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (cédula, documento) que presenta error al generar soporte CAC. FK a INPACIENT. Identificación PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del paciente que presenta error al generar soporte CAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación con tabla HCSOPORTECACPACIENTE. Vincula detalle a paciente específico en evento de soporte CAC (FK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECACPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de relacion con la tabla de paciente de soporte CAC (HCSOPORTECACPACIENT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECACPACIENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECACPACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID cabecera/encabezado de soporte CAC desde tabla HCSOPORTECAC (FK). Agrupa detalles de error por solicitud de soporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera de soporte CAC (HCSOPORTECAC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'IDHCSOPORTECAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (Identity INT) de cada registro de detalle en tabla de soporte CAC de paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensajes o detalles de soporte del Centro de Atención al Cliente (CAC) asociados a un paciente específico. Registra cada mensaje enviado o recibido dentro de un caso de soporte CAC para un paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCSOPORTECACPACIENTED';
