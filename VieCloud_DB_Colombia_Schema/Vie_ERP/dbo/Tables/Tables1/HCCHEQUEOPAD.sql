CREATE TABLE [dbo].[HCCHEQUEOPAD] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES]       CHAR (10)     NOT NULL,
    [IPCODPACI]       VARCHAR (25)  NOT NULL,
    [IDLISTACHEQUEO]  INT           NOT NULL,
    [IDLISTACHEQUEOD] INT           NOT NULL,
    [CHEQUEADO]       BIT           NOT NULL,
    [CAMPOTEXTO]      VARCHAR (500) NULL,
    [CAMPOFECHA]      DATETIME      NULL,
    [URLADJUNTO]      VARCHAR (100) NULL,
    CONSTRAINT [PK_HCCHEQUEOPAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCCHEQUEOPAD_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCHEQUEOPAD_HCLISTACC] FOREIGN KEY ([IDLISTACHEQUEO]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC]),
    CONSTRAINT [FK_HCCHEQUEOPAD_HCLISTACD] FOREIGN KEY ([IDLISTACHEQUEOD]) REFERENCES [dbo].[HCLISTACD] ([CODCONSEC]),
    CONSTRAINT [FK_HCCHEQUEOPAD_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del archivo adjunto asociado al resultado del chequeo; referencia a documento, imagen o evidencia digital', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado campo Adjunto, URL del adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'URLADJUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora registrada como resultado del item de chequeo; datetime para auditoría y trazabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado campo fecha ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CAMPOFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto libre para resultado, observación o nota clínica del item de chequeo; varchar(500)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado campo fecha ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CAMPOTEXTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario si el item de la lista de chequeo fue completado/validado; bit 0=no, 1=sí', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si fue seleccionada la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'CHEQUEADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del item detalle de la lista de chequeo; referencia FK a HCLISTACD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Item deatlle de la lista de Chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la lista de chequeo principal; referencia FK a HCLISTACC para agrupar items', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IDLISTACHEQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente; cédula, identificación o documento único del paciente; FK a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención; identificador del episodio de hospitalización o consulta; FK a ADINGRESO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de chequeo padronizado; clave primaria identity(1,1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificaión del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de respuestas y verificaciones de listas de chequeo (checklists) aplicadas a pacientes durante su ingreso hospitalario. Almacena si cada ítem fue chequeado, junto con valores de texto, fecha y archivos adjuntos asociados a cada verificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCHEQUEOPAD';
