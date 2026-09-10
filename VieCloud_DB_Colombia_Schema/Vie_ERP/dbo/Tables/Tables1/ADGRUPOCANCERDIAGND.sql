CREATE TABLE [dbo].[ADGRUPOCANCERDIAGND] (
    [ID]               INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADGRUPOCANCERC] INT      NOT NULL,
    [CODDIAGNO]        CHAR (4) NOT NULL,
    [LymphomaType]     INT      NULL,
    CONSTRAINT [PK_ADGRUPOCANCERDIAGND] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADGRUPOCANCERDIAGND_ADGRUPOCANCERC] FOREIGN KEY ([IDADGRUPOCANCERC]) REFERENCES [dbo].[ADGRUPOCANCERC] ([ID]),
    CONSTRAINT [FK_ADGRUPOCANCERDIAGND_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);




GO



GO





GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico oncológico (cáncer), tipo CHAR(4), referencia a tabla INDIAGNOS. Identifica el tipo de cáncer diagnosticado mediante código CIE-10 o equivalente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de cáncer (FK a ADGRUPOCANCERC), vincula el diagnóstico a su clasificación oncológica. Entero, clave foránea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'IDADGRUPOCANCERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de grupos canceres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'IDADGRUPOCANCERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'IDADGRUPOCANCERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY INT), consecutivo/número secuencial de registro en la tabla de diagnósticos de cáncer. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de linfoma: 1=Hodgkin, 2=No Hodgkin. Campo opcional (NULL permitido), INT. Subclasificación de linfomas dentro del grupo de cáncer hematológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'LymphomaType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Linfoma:

1: Hodgkin 
2: No Hodgkin 
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'LymphomaType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'LymphomaType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los diagnósticos CIE-10 asociados a grupos de cáncer para la clasificación oncológica del paciente. Permite vincular cada grupo de cáncer con sus códigos de diagnóstico específicos y el tipo de linfoma cuando aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND';
