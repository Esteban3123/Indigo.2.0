CREATE TABLE [dbo].[OthersMaterialsProgQX] (
    [ID]                  INT        IDENTITY (1, 1) NOT NULL,
    [IDAGEPROGQX]         INT        NULL,
    [MATERIALDESCRIPTION] CHAR (300) NULL,
    [QUANTITY]            INT        NULL,
    CONSTRAINT [PK__OthersMa__3214EC27060FB890] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica del material adicional programado para la intervención quirúrgica; unidades de insumo quirúrgico requeridas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'QUANTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del material adicional programado para la cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'QUANTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'QUANTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual del material adicional programado para la cirugía; nombre del insumo, implante o dispositivo médico quirúrgico (max 300 caracteres).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'MATERIALDESCRIPTION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del material adicional programado para la cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'MATERIALDESCRIPTION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'MATERIALDESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la agenda de programación quirúrgica (AGEPROGQX); clave foránea que vincula el material a la cirugía programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la fila; consecutivo autoincrementable de materiales adicionales quirúrgicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Materiales adicionales u otros insumos asociados a una programación quirúrgica. Registra los materiales especiales o extras que se requieren para cada procedimiento quirúrgico programado, junto con la cantidad solicitada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OthersMaterialsProgQX';
