CREATE TABLE [dbo].[ODONTOOTROSTRA] (
    [ID]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOCONTROL] INT            NOT NULL,
    [CONSECTRA]       INT            NOT NULL,
    [OBSERVTRA]       NVARCHAR (250) NULL,
    [CODSERIPS]       CHAR (20)      CONSTRAINT [DF_ODONTOOTROSTRA_CODSERIPS] DEFAULT ((232101)) NULL,
    [CANTIDAD]        INT            NULL,
    CONSTRAINT [PK_ODONTOOTROSTRA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOOTROSTRA_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ODONTOOTROSTRA_ODONTOCONTROL] FOREIGN KEY ([IDODONTOCONTROL]) REFERENCES [dbo].[ODONTOCONTROL] ([ID]),
    CONSTRAINT [FK_ODONTOOTROSTRA_ODOPARTRA1] FOREIGN KEY ([CONSECTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del tratamiento odontológico enviado/facturado. Número de servicios o procedimientos prestados. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me especifica la cantidad de tratamiento que se envio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (procedimiento odontológico) seleccionado por el odontólogo para facturación. Puede estar vacío si el tratamiento agregado carece de CUPS parametrizado. FK a INCUPSIPS. CHAR(20). Default: 232101.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CUPS que selecciono el medico para realizar la facturación, en ocasiones es vacio ya que el tratamiento que se agrega NO tiene parametrizado algun CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de observaciones, notas clínicas o comentarios específicos de cada tratamiento odontológico. NVARCHAR(250) nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'OBSERVTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de observacion de cada tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'OBSERVTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'OBSERVTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla ODOPARTRA (parámetros de tratamiento). FK CONSECTRA. INT not null.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de relacion con la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'CONSECTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con control odontológico (ODONTOCONTROL). Vincula el tratamiento al registro de control/evaluación del odontograma. FK. INT not null.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de relacion con la tabla de control de odontograma ODONTOGCTR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (primary key) del registro de tratamiento odontológico. IDENTITY(1,1). INT not null.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de otros tratamientos odontológicos asociados a un control o consulta dental, incluyendo los procedimientos adicionales realizados, su cantidad y observaciones clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOOTROSTRA';
