CREATE TABLE [dbo].[HCATINPAR] (
    [IDETIPHIS]               CHAR (9)       NOT NULL,
    [CODPROSAL]               CHAR (20)      NOT NULL,
    [NUMEFOLIO]               CHAR (10)      NOT NULL,
    [IPCODPACI]               VARCHAR (25)   NOT NULL,
    [NUMINGRES]               CHAR (10)      NOT NULL,
    [CODCENATE]               CHAR (10)      NOT NULL,
    [UFUCODIGO]               CHAR (10)      NOT NULL,
    [FECINIATE]               DATETIME       NOT NULL,
    [INITRAPAR]               VARCHAR (50)   NULL,
    [TERTRAPAR]               VARCHAR (50)   NULL,
    [PRESENTAC]               VARCHAR (50)   NULL,
    [NUMEROFET]               INT            NULL,
    [TIERUPMEM]               NUMERIC (5, 1) NULL,
    [DESLIQAMN]               VARCHAR (50)   NULL,
    [EPISIOTIM]               BIT            NULL,
    [PREDESGAR]               VARCHAR (10)   NULL,
    [VALOGRADO]               INT            NULL,
    [CONTAPIEL]               VARCHAR (50)   NULL,
    [PINZACORD]               VARCHAR (50)   NULL,
    [ALUMBRACT]               BIT            NULL,
    [TIPALUMBR]               VARCHAR (50)   NULL,
    [DESPLACEN]               VARCHAR (50)   NULL,
    [REVISUTER]               BIT            NULL,
    [CANTSANGR]               INT            NULL,
    [CODIGROJO]               BIT            NULL,
    [OBSERVACI]               VARCHAR (2000) NULL,
    [INDIEPISI]               VARCHAR (500)  NULL,
    [ANALISISP]               VARCHAR (MAX)  NULL,
    [INDICAPAC]               CHAR (2)       NOT NULL,
    [INDICAMED]               VARCHAR (MAX)  NOT NULL,
    [INDAUDFOR]               NUMERIC (18)   NOT NULL,
    [MOSEPICRI]               BIT            NULL,
    [PLAINDMED]               VARCHAR (MAX)  NULL,
    [FECHINIHI]               DATETIME       NULL,
    [MultiplePregnancy]       BIT            NULL,
    [Analgesia]               VARCHAR (50)   NULL,
    [FamilyAccompaniment]     BIT            NULL,
    [CODESPECI]               CHAR (3)       NULL,
    [ChildbirthDateAndTime]   DATETIME       NULL,
    [CompanionName]           VARCHAR (100)  NULL,
    [CompanionRelationship]   VARCHAR (50)   NULL,
    [PreparationCourse]       INT            NULL,
    [ReceivedAnalgesia]       BIT            NULL,
    [AnesthesiaType]          VARCHAR (50)   NULL,
    [SkinToSkinBreastfeeding] BIT            NULL,
    [WhyNotBreastfeed]        VARCHAR (50)   NULL,
    CONSTRAINT [PK_HCATINPAR] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCATINPAR_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCATINPAR_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCATINPAR_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCATINPAR_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCATINPAR_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCATINPAR_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCATINPAR].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCATINPAR].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCATINPAR].[ANALISISP]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCATINPAR].[INDICAMED]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
CREATE NONCLUSTERED INDEX [IX_HCATINPAR__IPCODPACI__NUMINGRES__NUMEFOLIO]
    ON [dbo].[HCATINPAR]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC);


GO
ALTER INDEX [IX_HCATINPAR__IPCODPACI__NUMINGRES__NUMEFOLIO]
    ON [dbo].[HCATINPAR] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo por el que no se realizó lactancia materna piel con piel; registrado desde página Datos del parto en HC Atención del parto (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'WhyNotBreastfeed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la causa de por que no huvo lactancia piel con piel registrado desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'WhyNotBreastfeed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'WhyNotBreastfeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de lactancia materna piel con piel realizada; registrado desde página Datos del parto en HC Atención del parto (BIT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'SkinToSkinBreastfeeding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el registro de lactancia piel con piel desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'SkinToSkinBreastfeeding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'SkinToSkinBreastfeeding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de anestesia o analgesia aplicada durante el parto; registrado desde página Datos del parto en HC Atención del parto (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'AnesthesiaType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el tipo de analgesia registrado desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'AnesthesiaType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'AnesthesiaType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si la madre recibió analgesia durante el trabajo de parto; registrado desde página Datos del parto (BIT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ReceivedAnalgesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si recibio analgesia desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ReceivedAnalgesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ReceivedAnalgesia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de curso de preparación para el parto asistido por la gestante; desde página Datos del parto en HC Atención del parto (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PreparationCourse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el curso de preparacion registrado desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PreparationCourse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PreparationCourse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación o parentesco del acompañante durante el parto; registrado desde página Datos del parto en HC Atención del parto (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CompanionRelationship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el parentesco del acompañate registrado desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CompanionRelationship';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CompanionRelationship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del acompañante durante el parto; registrado desde página Datos del parto en HC Atención del parto (VARCHAR 100, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CompanionName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el nombre del acompañate registrado desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CompanionName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CompanionName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta del nacimiento del producto; registrado en campo fecha y hora de parto de HC Atención del parto (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ChildbirthDateAndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha y hora registrada en el campo "fecha y hora de parto" del page de datos del parto de la hc atención parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ChildbirthDateAndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ChildbirthDateAndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica (pediatría, obstetricia, etc.) asociada a la firma de la historia clínica; vinculado a tabla INESPECIA (CHAR 3, FK, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el código de la especialidad con la que se firmó la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de acompañamiento familiar durante el parto; 1=Sí, 0=No (BIT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'FamilyAccompaniment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si lo acompaña un familiar  1 = Si  0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'FamilyAccompaniment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'FamilyAccompaniment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de analgesia o medicamento anestésico administrado (ej: Medicamentos, Peridural); campo obsoleto desde 09/09/2025 PBI 27151 (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'Analgesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo analgesia:   Medicamentos  Peridural (Obsoleto 09/09/2025 PBI 27151) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'Analgesia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'Analgesia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de embarazo múltiple (gemelar, triple, etc.); 1=Sí, 0=No (BIT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'MultiplePregnancy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo embarazo multiple', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'MultiplePregnancy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'MultiplePregnancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicialización o creación del registro en la historia clínica de atención del parto (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'FECHINIHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Inicalizacion de la Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'FECHINIHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'FECHINIHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla o esquema de indicaciones médicas predefinidas al paciente postparto (VARCHAR MAX, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de indicaciones medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PLAINDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador para mostrar datos en Epicrisis (resumen clínico al egreso); 1=Mostrar, 0=No mostrar (BIT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar en  Epicrisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de control y auditoría sobre acciones registradas en la historia; usado para trazabilidad (NUMERIC 18, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones médicas generales al paciente postparto, enmascaradas como Instructions_Ofuscado (PII); (VARCHAR MAX, masked, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones Medicas Generales al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDICAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de destino del paciente al egreso: 1=Urgencias, 2=Observación Urgencias, 3=Hospitalización, 4=UCI Adulto, 5=UCI Pediátrica, 6=UCI Neonatal, 7=Consulta Externa, 8=Cirugía, 9=Hospitalización en Casa, 10=Referencia, 11=Morgue, 12=Salida, 13=Continúa en Unidad (CHAR 2, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el destino del paciente  1. Trasladar a Urgencias: Solo consulta externa  2. Trasladar a Observacion Urgencias: solo Urgencias  3. Trasladar a Hospitalizacion: Dif Misma Unidad  4. Trasladar a  UCI Adulto: Dif Misma Unidad  5. Trasladar a UCI Pediatrica: Dif Misma Unidad  6. Trasladar a UCI Neonatal: Dif Misma Unidad  7. Trasladar a Consulta Externa: Dif Misma Unidad  8. Trasladar a  Cirugia: Dif Misma Unidad  9. Hospitalizacion en Casa  10. Referencia  11. Morgue  12. Salida  13. Continua en la Unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDICAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Análisis clínico del parto, enmascarado como Analysis_Ofuscado (PII); puede contener hallazgos perinatales (VARCHAR MAX, masked, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ANALISISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ANALISISP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ANALISISP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones obstétricas específicas relacionadas con episiotomía o trauma perineal (VARCHAR 500, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDIEPISI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDIEPISI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDIEPISI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales y notas clínicas adicionales del proceso de atención del parto (VARCHAR 2000, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de Código Rojo o emergencia obstétrica durante el parto; 1=Sí, 0=No (BIT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODIGROJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Rojo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODIGROJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODIGROJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad estimada de sangrado en centímetros cúbicos (cc) durante el parto y alumbramiento (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CANTSANGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sangrado C.C.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CANTSANGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CANTSANGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de revisión uterina manual realizada postparto; 1=Sí, 0=No (BIT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'REVISUTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Revision Uterina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'REVISUTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'REVISUTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del estado, tipo y características de expulsión de la placenta; normal, retenida, parcial, etc. (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'DESPLACEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Placenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'DESPLACEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'DESPLACEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de alumbramiento ocurrido: espontáneo, manual, quirúrgico, etc. (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'TIPALUMBR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Alumbramiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'TIPALUMBR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'TIPALUMBR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de alumbramiento activo (manejo activo de la tercera etapa del parto); 1=Sí, 0=No (BIT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ALUMBRACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Alumbramiento Activo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ALUMBRACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ALUMBRACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o indicador del pinzamiento del cordón umbilical: tiempo, técnica (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PINZACORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pinzamiento del Cordon', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PINZACORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PINZACORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del contacto piel con piel entre madre e hijo después del nacimiento (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CONTAPIEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contacto Piel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CONTAPIEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CONTAPIEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado o escala de severidad en caso de trauma perineal o complicación del parto (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'VALOGRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'VALOGRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'VALOGRADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de desgarro perineal: 1=Sí, 2=No, 3=No aplica (VARCHAR 10, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PREDESGAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desgarro:  1: SI  2: NO  3: NO APLICA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PREDESGAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PREDESGAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de episiotomía practicada durante el expulsivo; 1=Sí, 0=No (BIT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'EPISIOTIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Episiotimia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'EPISIOTIM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'EPISIOTIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del líquido amniótico: claro, meconial, hemático, etc. (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'DESLIQAMN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Liquido Amniotico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'DESLIQAMN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'DESLIQAMN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo en horas transcurrido desde la ruptura de membranas hasta el parto; usado para detectar corioamnionitis (NUMERIC 5.1, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'TIERUPMEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Ruptura Membranas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'TIERUPMEM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'TIERUPMEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de fetos expulsados en parto múltiple (INT, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'NUMEROFET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de fetos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'NUMEROFET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'NUMEROFET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presentación fetal al parto: cefálica, pelviana, transversa, etc. (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PRESENTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presentacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PRESENTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PRESENTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de termino o finalización del trabajo de parto activo (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'TERTRAPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Termina Trabajo de Parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'TERTRAPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'TERTRAPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio del trabajo de parto registrado clínicamente (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INITRAPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inicio Trabajo de Parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INITRAPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INITRAPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de atención del evento de parto en el centro de salud (DATETIME, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'FECINIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'FECINIATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'FECINIATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (sala de parto, quirófano obstétrico, etc.) donde se atendió el parto; referencia a INUNIFUNC (CHAR 10, FK, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o institución de salud donde ocurrió el parto; referencia a ADCENATEN (CHAR 10, FK, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único del ingreso hospitalario del paciente; vinculado a tabla ADINGRESO (CHAR 10, FK, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (equivalente a cédula/identificación), enmascarado como Identification_Ofuscado (PII); referencia a INPACIENT (VARCHAR 25, masked, FK, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de folio de la historia clínica de atención del parto (CHAR 10, PK, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico/partera) que firmó la historia, enmascarado como Identification_Ofuscado (PII); referencia a INPROFSAL (CHAR 20, masked, FK, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno del tipo de historia clínica; clasificación de registro en el EHR (CHAR 9, NOT NULL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro clínico de atención del parto: guarda los datos del trabajo de parto y parto de una paciente, incluyendo información del profesional que atendió, evolución del trabajo de parto, alumbramiento, episiotomía, desgarros, sangrado, contacto piel a piel, lactancia materna y acompañamiento familiar, asociados a un ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR';
