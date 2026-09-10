CREATE TABLE [dbo].[ANTVALORES] (
    [ID]             INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA]    INT            NOT NULL,
    [CODANTECEDENTE] INT            NOT NULL,
    [IDANTVARIABLE]  INT            NOT NULL,
    [VALOR]          VARCHAR (5000) NOT NULL,
    [IDITEMLISTA]    INT            NULL,
    CONSTRAINT [PK_ANTVALORES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ANTVALORES_ANTVARIABLES] FOREIGN KEY ([IDANTVARIABLE]) REFERENCES [dbo].[ANTVARIABLES] ([ID]),
    CONSTRAINT [FK_ANTVALORES_ANTVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[ANTVARIABLESL] ([ID]),
    CONSTRAINT [FK_ANTVALORES_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_ANTVALORES_RSVALORES] FOREIGN KEY ([ID]) REFERENCES [dbo].[ANTVALORES] ([ID]),
    CONSTRAINT [FK_ANTVALORES_RSVARIABLES] FOREIGN KEY ([IDANTVARIABLE]) REFERENCES [dbo].[ANTVARIABLES] ([ID])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del elemento de lista seleccionado cuando la variable de antecedente es de tipo lista/catálogo. FK a ANTVARIABLESL. INT NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si la variable es una lista guarda aqui el ID del Item seleccioado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido o respuesta capturada de la variable de antecedente; puede ser texto libre, número, fecha u otra representación según tipo de variable. VARCHAR(5000).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el valor ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la variable dinámica de antecedentes asociada al valor registrado. FK a ANTVARIABLES(ID). INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDANTVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del variable Dinamico Antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDANTVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDANTVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código clasificatorio del tipo de antecedente: 1=Médicos, 2=Quirúrgicos, 3=Anestésicos, 4=Transfusionales, 5=Inmunológicos, 6=Alérgicos, 7=Traumáticos, 8=Psicológicos, 9=Farmacológicos, 10=Familiares, 11=Gineco-Obstétricos, 12=Urológico-Sexual, 13=Perinatales, 14=Tóxicos, 15=Hábitos de Vida, 16=Esquema de Vacunación, 17=Escolares, 18=Laborales, 19=Nutricionales, 20=Odontológicos, 21=Socioeconómicos, 22=Otros. INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Antecedente:  1-Médicos  2-Quirurgicio  3-Anestésico  4-Transfunsionales  5-Inmunológicos  6-Alérgicos  7-Traumáticos  8-Psicológicos  9-Farmacológicos  10-Familiares  11-Gineco-Obstétricos  12-Urológico-sexual  13-Perinatales  14-Tóxicos  15-Hábitos de Vida  16-Esquema de Vacunación  17-Escolores  18-Laborales  19-Nutricionales  20-Odontológicos  21-SocioEconómicos  22- Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la historia clínica del paciente/atención en la tabla HCHISPACA. FK a HCHISPACA(ID). INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del registro que guarda en la tabla HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de valor de antecedente. IDENTITY INT, secuencial incremental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores registrados para los antecedentes clínicos del paciente en la historia clínica. Cada fila almacena la respuesta o dato ingresado para una variable específica de un antecedente (personal, familiar, quirúrgico, alérgico, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ANTVALORES';
