CREATE TABLE [dbo].[ADFURPROU] (
    [NUMRADANT]  CHAR (10)                                                                    NULL,
    [RESPGLOSA]  CHAR (1)                                                                     NULL,
    [NUMINGRES]  CHAR (10)                                                                    NOT NULL,
    [NUMFACTUR]  CHAR (20)                                                                    NOT NULL,
    [CODIPSSEC]  CHAR (12)                                                                    NOT NULL,
    [PRIAPEVIC]  CHAR (20)                                                                    NOT NULL,
    [SEGAPEVIC]  CHAR (30)                                                                    NULL,
    [PRINOMVIC]  CHAR (20)                                                                    NOT NULL,
    [SEGNOMVIC]  CHAR (30)                                                                    NULL,
    [TIPDOCVIC]  CHAR (2)                                                                     NOT NULL,
    [NUMDOCVIC]  VARCHAR (25)                                                                 NULL,
    [FECNACVIC]  DATETIME                                                                     NOT NULL,
    [SEXOVICTI]  CHAR (1)                                                                     NOT NULL,
    [DIRRESVIC]  CHAR (40)                                                                    NOT NULL,
    [CODDEPVIC]  CHAR (2)                                                                     NOT NULL,
    [CODMUNVIC]  CHAR (3)                                                                     NOT NULL,
    [NUMTELVIC]  CHAR (10)                                                                    NOT NULL,
    [CODAFIVIC]  CHAR (1)                                                                     NOT NULL,
    [REGACTVIC]  CHAR (1)                                                                     NULL,
    [CODEPSVIC]  CHAR (6)                                                                     NULL,
    [NATUEVENT]  CHAR (2)                                                                     NOT NULL,
    [DESOTREVE]  CHAR (25)                                                                    NULL,
    [DIROCUEVE]  CHAR (40)                                                                    NOT NULL,
    [FECOCUEVE]  DATETIME                                                                     NOT NULL,
    [CODDEPEVE]  CHAR (2)                                                                     NOT NULL,
    [CODMUNEVE]  CHAR (3)                                                                     NOT NULL,
    [CODZONEVE]  CHAR (1)                                                                     NOT NULL,
    [DESBREEVEN] CHAR (255)                                                                   NOT NULL,
    [CODDIAPRI]  CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)') NOT NULL,
    [CODDIAAS1]  CHAR (4)                                                                     NOT NULL,
    [CODDIAAS2]  CHAR (4)                                                                     NOT NULL,
    [CODDIAAS3]  CHAR (4)                                                                     NOT NULL,
    [CODDIAAS4]  CHAR (4)                                                                     NOT NULL,
    [DESPROTES]  CHAR (100)                                                                   NOT NULL,
    [VALRECPRO]  CHAR (15)                                                                    NOT NULL,
    [VALADAPRO]  CHAR (15)                                                                    NOT NULL,
    [VALREAPRO]  CHAR (15)                                                                    NOT NULL,
    [VALTOTREC]  CHAR (15)                                                                    NOT NULL,
    [NUMTOTFOL]  CHAR (3)                                                                     NOT NULL,
    [FECRADFUR]  DATETIME                                                                     NOT NULL,
    CONSTRAINT [PK_ADFURPROU] PRIMARY KEY CLUSTERED ([NUMFACTUR] ASC),
    CONSTRAINT [FK_ADFURPROU_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADFURPROU_INPACIENT] FOREIGN KEY ([NUMDOCVIC]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADFURPROU] NOCHECK CONSTRAINT [FK_ADFURPROU_ADINGRESO];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADFURPROU].[CODDIAPRI]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de radicación de la reclamación ante la entidad aseguradora o EPS, timestamp de presentación oficial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECRADFUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha radicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECRADFUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECRADFUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de folios o páginas que conforman el expediente de la reclamación, cantidad de documentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMTOTFOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMTOTFOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMTOTFOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total reclamado en moneda local (COP), suma de todos los conceptos de protesis, adaptación y rehabilitación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALTOTREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Total Reclamado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALTOTREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALTOTREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor reclamado por rehabilitación, monto solicitado para servicios de terapia y recuperación funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALREAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Reclamado Rehabilitacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALREAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALREAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor reclamado por adaptación, monto solicitado para ayudas técnicas y elementos de acomodación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALADAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Reclamado Adaptacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALADAPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALADAPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor reclamado por protesis, monto solicitado para dispositivos protésicos y ortopédicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALRECPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Reclamado Protesis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALRECPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'VALRECPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la protesis o dispositivo ortopédico reclamado, especificaciones técnicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESPROTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Protesis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESPROTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESPROTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico asociado 4 (comorbilidad o diagnóstico secundario tercer complementario), clasificación CIE-10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código asociado 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico asociado 3 (comorbilidad o diagnóstico secundario segundo complementario), clasificación CIE-10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código asociado 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico asociado 2 (comorbilidad o diagnóstico secundario primer complementario), clasificación CIE-10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código asociado 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico asociado 1 (comorbilidad o diagnóstico secundario), clasificación CIE-10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código asociado 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAAS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico principal de la lesión o enfermedad que origina la reclamación, CIE-10, campo ofuscado por PII', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código diagnóstico principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDIAPRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción breve del evento accidente o acontecimiento que genera la lesión, resumen del incidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESBREEVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESBREEVEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESBREEVEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de zona geográfica donde ocurrió el evento (urbano, rural, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODZONEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código zona evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODZONEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODZONEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de municipio o ciudad donde ocurrió el evento accidente, ubicación territorial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODMUNEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código municipio evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODMUNEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODMUNEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de departamento o entidad territorial donde ocurrió el evento, ubicación geográfica superior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDEPEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo departamento evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDEPEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDEPEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de ocurrencia del evento accidente, timestamp del suceso lesionante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECOCUEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Evento Accidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECOCUEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECOCUEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección completa del lugar donde ocurrió el evento accidente, ubicación física específica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DIROCUEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DIROCUEVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DIROCUEVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otro tipo de evento no clasificado en las categorías estándar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESOTREVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESOTREVE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DESOTREVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza del evento accidente (trauma, enfermedad profesional, accidente de tránsito, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NATUEVENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NATUEVENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NATUEVENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la EPS o entidad aseguradora del victimario o responsable del accidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODEPSVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Eps', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODEPSVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODEPSVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Régimen de afiliación del victimario (contributivo, subsidiado, especial, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'REGACTVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Regimen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'REGACTVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'REGACTVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de afiliación o estado de afiliación del victimario ante sistema de seguridad social', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODAFIVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Afiliacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODAFIVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODAFIVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono de contacto del victimario, 10 dígitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMTELVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'número de teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMTELVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMTELVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de municipio de residencia del victimario, ubicación territorial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODMUNVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODMUNVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODMUNVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de departamento de residencia del victimario, ubicación geográfica superior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDEPVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDEPVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODDEPVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de residencia del victimario, ubicación física domiciliaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DIRRESVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DIRRESVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'DIRRESVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo del victimario (M/F), característica demográfica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEXOVICTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'sexo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEXOVICTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEXOVICTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del victimario, cálculo de edad y datos biográficos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECNACVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECNACVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'FECNACVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de documento de identidad del victimario (cédula, pasaporte, etc.), identificación PII, FK a INPACIENT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'número de documento ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación del victimario (CC, CE, PA, etc.), categoría del documento de identidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'TIPDOCVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tipo identificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'TIPDOCVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'TIPDOCVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del victimario, parte del nombre completo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEGNOMVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'segundo nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEGNOMVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEGNOMVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del victimario, componente principal del nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'PRINOMVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'PRINOMVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'PRINOMVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del victimario, parte de la identificación nominal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEGAPEVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEGAPEVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'SEGAPEVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del victimario, componente principal del apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'PRIAPEVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'PRIAPEVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'PRIAPEVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la IPS ante la Secretaría Departamental de Salud, identificador de prestador de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la IPS ante la Secretaria Departamental de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de factura de la reclamación, identificador único de documento fiscal, PK de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMFACTUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de factura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMFACTUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMFACTUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso hospitalario o atención, FK referencia a ADINGRESO, vinculación a episodio clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta a glosa, indicador de resolución (aprobado, parcialmente aprobado, rechazado, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'RESPGLOSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'RESPGLOSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'RESPGLOSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicación anterior o previo, referencia a reclamación relacionada en historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMRADANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No radicado anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMRADANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU', @level2type = N'COLUMN', @level2name = N'NUMRADANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de eventos de urgencias y accidentes (FURIPS/FURAP) asociados a una facturación e ingreso. Guarda los datos de la víctima, el lugar y naturaleza del evento, los diagnósticos relacionados y los valores de la atención prestada para el reporte obligatorio a aseguradoras o entidades de control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURPROU';
