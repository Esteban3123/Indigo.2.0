CREATE TABLE [dbo].[HCFICHA352] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [COMPLESERVI]         INT           NULL,
    [SERVIADMIN]          INT           NULL,
    [PROCEDMEDI]          INT           NULL,
    [DIABETES]            BIT           NULL,
    [INMUNOSUPRE]         BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [DESNUTRICION]        BIT           NULL,
    [PREECLAMPSIA]        BIT           NULL,
    [ANEMIA]              BIT           NULL,
    [CLASIFASA]           INT           NULL,
    [TIPOHERIDA]          INT           NULL,
    [DURAPROCE]           INT           NULL,
    [SUPERPRIMA]          BIT           NULL,
    [SUPERSECUN]          BIT           NULL,
    [PROFUPRIMA]          BIT           NULL,
    [PROFUSECUN]          BIT           NULL,
    [ORGAESPAC]           BIT           NULL,
    [PROFILAXIS]          BIT           NULL,
    [TIEMPOADMIN]         INT           NULL,
    [LAPAROSCOPIA]        BIT           NULL,
    [FECHATOMA1]          DATE          NULL,
    [FECHATOMA2]          DATE          NULL,
    [FECHATOMA3]          DATE          NULL,
    [CODMUESTRA1]         INT           NULL,
    [CODMUESTRA2]         INT           NULL,
    [CODMUESTRA3]         INT           NULL,
    [CODPRUEBA1]          INT           NULL,
    [CODPRUEBA2]          INT           NULL,
    [CODPRUEBA3]          INT           NULL,
    [MICROORGA1]          VARCHAR (50)  NULL,
    [MICROORGA2]          VARCHAR (50)  NULL,
    [MICROORGA3]          VARCHAR (50)  NULL,
    [CUAL]                VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA352] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA352_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA352_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA352] NOCHECK CONSTRAINT [CK_HCFICHA352_JSON];




GO
ALTER TABLE [dbo].[HCFICHA352] NOCHECK CONSTRAINT [CK_HCFICHA352_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales y dinámicos en formato JSON, validado con constraint CHECK(isjson), permite extensibilidad de nuevas columnas sin alterar estructura física de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión o iteración de la ficha de notificación, control de cambios y compatibilidad de formato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Version de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto libre para especificar ''''cuál'''' (texto descriptivo adicional, complemento de opciones predefinidas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en tercera muestra, resultado de cultivo o biopsia (tipo VARCHAR 50, ej: Escherichia coli, Staphylococcus aureus)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'MICROORGA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'MICROORGA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'MICROORGA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en segunda muestra, resultado de cultivo o biopsia (tipo VARCHAR 50, ej: Escherichia coli, Staphylococcus aureus)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'MICROORGA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'MICROORGA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'MICROORGA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Microorganismo aislado en primera muestra, resultado de cultivo o biopsia (tipo VARCHAR 50, ej: Escherichia coli, Staphylococcus aureus)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'MICROORGA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Microorganismo aislado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'MICROORGA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'MICROORGA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de prueba diagnóstica en tercera muestra: 1=Cultivo, 2=Hemocultivo, 3=Biopsia, 4=Radiografía, 5=TAC (tomografía axial computarizada), 6=RNM (resonancia nuclear magnética)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la prueba:  1=Cultivo   2=Hemocultivo   3=Biopsia   4=Radiografía   5=Tomografía axial computarizada   6=Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de prueba diagnóstica en segunda muestra: 1=Cultivo, 2=Hemocultivo, 3=Biopsia, 4=Radiografía, 5=TAC (tomografía axial computarizada), 6=RNM (resonancia nuclear magnética)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la prueba:  1=Cultivo   2=Hemocultivo   3=Biopsia   4=Radiografía   5=Tomografía axial computarizada   6=Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de prueba diagnóstica en primera muestra: 1=Cultivo, 2=Hemocultivo, 3=Biopsia, 4=Radiografía, 5=TAC (tomografía axial computarizada), 6=RNM (resonancia nuclear magnética)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la prueba:  1=Cultivo   2=Hemocultivo   3=Biopsia   4=Radiografía   5=Tomografía axial computarizada   6=Renonancia nuclear magnética', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODPRUEBA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de muestra biológica en tercera toma: 1=Sangre, 2=Tejido, 3=Otros líquidos estériles (LCR, pleural), 4=Secreciones (urinaria, respiratoria, herida)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la muestra  1=Sangre  2=Tejido  3=Otros líquidos estériles  4=Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de muestra biológica en segunda toma: 1=Sangre, 2=Tejido, 3=Otros líquidos estériles (LCR, pleural), 4=Secreciones (urinaria, respiratoria, herida)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la muestra  1=Sangre  2=Tejido  3=Otros líquidos estériles  4=Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de muestra biológica en primera toma: 1=Sangre, 2=Tejido, 3=Otros líquidos estériles (LCR, pleural), 4=Secreciones (urinaria, respiratoria, herida)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la muestra  1=Sangre  2=Tejido  3=Otros líquidos estériles  4=Secreciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODMUESTRA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la tercera muestra biológica para examen de laboratorio o microbiología (type DATE, requisito de infección nosocomial)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'FECHATOMA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la segunda muestra biológica para examen de laboratorio o microbiología (type DATE, requisito de infección nosocomial)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'FECHATOMA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de toma de la primera muestra biológica para examen de laboratorio o microbiología (type DATE, requisito de infección nosocomial)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de toma de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'FECHATOMA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (procedimiento quirúrgico realizado con laparoscopia/mininvasivo), 0=No (abordaje abierto tradicional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'LAPAROSCOPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laparoscopia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'LAPAROSCOPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'LAPAROSCOPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento de administración de antibiótico profiláctico: 1=Antes del procedimiento, 2=Durante el procedimiento, 3=Después del procedimiento, 4=Ninguna administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'TIEMPOADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo en que se le administró el antibiótico:   1=Antes   2=Durante   3=Después   4=Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'TIEMPOADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'TIEMPOADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (recibió profilaxis antibiótica quirúrgica), 0=No (sin profilaxis)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFILAXIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profilaxis:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFILAXIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFILAXIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (órganos/espacios anatómicos comprometidos), 0=No. Relacionado con clasificación de profundidad de infección quirúrgica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ORGAESPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orgaespac:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ORGAESPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ORGAESPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (profundidad secundaria o infección superficial del sitio quirúrgico), 0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFUSECUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profusecun:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFUSECUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFUSECUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (profundidad primaria o infección profunda del sitio quirúrgico), 0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFUPRIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profuprima:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFUPRIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROFUPRIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (supervisión/infección secundaria detectada), 0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SUPERSECUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Supersecun:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SUPERSECUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SUPERSECUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (supervisión/infección primaria detectada post-procedimiento), 0=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SUPERPRIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Superprima:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SUPERPRIMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SUPERPRIMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración total del procedimiento quirúrgico o parto en minutos (type INT, variable dependiente de riesgo de infección)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DURAPROCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración del Procedimiento (minutos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DURAPROCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DURAPROCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de contaminación de herida quirúrgica: 1=Limpia, 2=Limpia contaminada, 3=Contaminada, 4=Sucia (infectada preoperatoria), según estándares de control de infecciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Herida:   1=Limpia   2=Limpia contaminada   3=Herida contaminada   4=Herida sucia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'TIPOHERIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación ASA de riesgo anestésico pre-operatorio: 1=ASA I (sano), 2=ASA II (enfermedad sistémica leve), 3=ASA III (enfermedad sistémica grave), 4=ASA IV (grave enfermedad sistémica amenaza vida), 5=ASA V (moribundo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CLASIFASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación ASA:   1=ASA 1   2=ASA 2   3=ASA 3   4=ASA 4   5=ASA 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CLASIFASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CLASIFASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (paciente con anemia pre-operatoria diagnosticada), 0=No. Factor de riesgo para complicaciones quirúrgicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ANEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anemia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ANEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ANEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (paciente embarazada con preeclampsia), 0=No. Complicación obstétrica grave que modifica riesgo de cesárea/parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Preeclampsia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (paciente con desnutrición diagnosticada), 0=No. Factor de riesgo para infección y cicatrización deficiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desnutricion:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DESNUTRICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (paciente con obesidad diagnosticada por IMC), 0=No. Comorbilidad que aumenta riesgo infeccioso quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obesidad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (paciente inmunosuprimido por enfermedad/medicamento), 0=No. Factor crítico de riesgo para infecciones nosocomiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmunosupre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'INMUNOSUPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: 1=Sí (paciente diabético tipo I o II), 0=No. Comorbilidad asociada a mayor riesgo de infección quirúrgica e impacto en cicatrización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diabetes:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del procedimiento médico-quirúrgico realizado: 1=Cesárea, 2=Herniorrafía (reparación hernia), 3=Parto vaginal, 4=Revascularización miocárdica (bypass), 5=Colecistectomía (extirpación vesícula), define riesgo basal de infección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROCEDMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Seleccione el procedimiento médico quirúrgico realizado:   1=Cesárea   2=Herniorrafía   3=Parto   4=Revascularización miocárdica con incisión torácica y del sitio donante   5=Colecistectomía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROCEDMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'PROCEDMEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio o contexto de admisión para procedimiento quirúrgico/parto: 1=Programado ambulatorio (cirugia mayor ambulatoria), 2=Urgencias (cirugía de emergencia), 3=Programado hospitalizado, afecta seguimiento y duración estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SERVIADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio de admisión del procedimiento quirúrgico o parto:   1=Programado ambulatorio   2=Urgencias   3=Programado hospitalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SERVIADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'SERVIADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complejidad del servicio médico-quirúrgico: 1=Baja complejidad, 2=Media complejidad, 3=Alta complejidad, influye en protocolos de prevención de infecciones nosocomiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'COMPLESERVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Complejidad del servicio médico quirúrgico:   1=Baja   2=Media   3=Alta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'COMPLESERVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'COMPLESERVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico clínico (CHAR 4, formato CIE-10 abreviado), diagnóstico principal o complicación relacionada con la ficha de notificación de infección intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ficha de notificación (FK referencia a [dbo].[HCFICHANOTIFICACION]), agrupa eventos de infección nosocomial de un paciente en una atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (IDENTITY 1,1) del registro en tabla HCFICHA352, clave primaria de la ficha de eventos clínicos quirúrgicos/obstétricos para notificación de infecciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha clínica 352 para vigilancia y notificación de infecciones del sitio operatorio (ISO). Registra factores de riesgo del paciente, clasificación de la cirugía, tipo de herida, profilaxis antibiótica, hallazgos microbiológicos de muestras tomadas y datos del procedimiento quirúrgico asociado a un evento de infección nosocomial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA352';
