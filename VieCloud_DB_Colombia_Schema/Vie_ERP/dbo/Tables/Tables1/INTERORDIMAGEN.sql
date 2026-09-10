CREATE TABLE [dbo].[INTERORDIMAGEN] (
    [ID]             INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDORDEN_IMAGEN] INT         NOT NULL,
    [CUPS]           CHAR (20)   NOT NULL,
    [TIPOORDEN]      VARCHAR (3) NOT NULL,
    [FECHACREACION]  DATETIME    NOT NULL,
    [CODUSUARIO]     CHAR (20)   NOT NULL,
    CONSTRAINT [PK_INTERORDIMAGEN] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario (profesional de la salud) que registra o crea la interconsulta de orden de imagen. Identificación del operador del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo usuario registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación/registro de la interconsulta de orden de imagen en el sistema. Auditoria de timestamp.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de orden de imagen: INT (Intrahospitalario/Hospitalización) o AMB (Ambulatorio). Indica contexto de atención donde se genera la interconsulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'TIPOORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Orden:  INT - Intrahospitalario  AMB - Ambulatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'TIPOORDEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'TIPOORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (20 caracteres) del procedimiento de diagnóstico por imagen enviado o solicitado. Clasificación de servicios de salud Colombia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'CUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo CUPS enviado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'CUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'CUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de imagen relacionada, referencia a HCORDIMAG (intrahospitalario) o AMBORDIMA (ambulatorio). Clave foránea que vincula con la orden de imagen original.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'IDORDEN_IMAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de orden de imagenes puede ser de la tabla (HCORDIMAG - AMBORDIMA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'IDORDEN_IMAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'IDORDEN_IMAGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT, PK) de la interconsulta de orden de imagen. Clave primaria de la tabla INTERORDIMAGEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las órdenes de imágenes diagnósticas (rayos X, ecografías, tomografías, resonancias, etc.) generadas para pacientes. Cada registro representa un servicio o procedimiento de imagen solicitado, identificado por su código CUPS, dentro de una orden médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERORDIMAGEN';
