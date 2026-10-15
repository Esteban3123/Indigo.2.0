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
ALTER TABLE [dbo].[HCATINPAR] NOCHECK CONSTRAINT [FK_HCATINPAR_ADCENATEN];


GO
ALTER TABLE [dbo].[HCATINPAR] NOCHECK CONSTRAINT [FK_HCATINPAR_ADINGRESO];


GO
ALTER TABLE [dbo].[HCATINPAR] NOCHECK CONSTRAINT [FK_HCATINPAR_INESPECIA];


GO
ALTER TABLE [dbo].[HCATINPAR] NOCHECK CONSTRAINT [FK_HCATINPAR_INPACIENT];


GO
ALTER TABLE [dbo].[HCATINPAR] NOCHECK CONSTRAINT [FK_HCATINPAR_INPROFSAL];


GO
ALTER TABLE [dbo].[HCATINPAR] NOCHECK CONSTRAINT [FK_HCATINPAR_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCATINPAR__IPCODPACI__NUMINGRES__NUMEFOLIO]
    ON [dbo].[HCATINPAR]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la causa de por que no huvo lactancia piel con piel registrado desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'WhyNotBreastfeed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el registro de lactancia piel con piel desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'SkinToSkinBreastfeeding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el tipo de analgesia registrado desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'AnesthesiaType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si recibio analgesia desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ReceivedAnalgesia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el curso de preparacion registrado desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PreparationCourse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el parentesco del acompañate registrado desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CompanionRelationship';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el nombre del acompañate registrado desde el page "Datos del parto" en la HC "Atención del parto"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CompanionName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la fecha y hora registrada en el campo "fecha y hora de parto" del page de datos del parto de la hc atención parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'ChildbirthDateAndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el código de la especialidad con la que se firmó la historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si lo acompaña un familiar  1 = Si  0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'FamilyAccompaniment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo analgesia:   Medicamentos  Peridural (Obsoleto 09/09/2025 PBI 27151) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'Analgesia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo embarazo multiple', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'MultiplePregnancy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define el destino del paciente  1. Trasladar a Urgencias: Solo consulta externa  2. Trasladar a Observacion Urgencias: solo Urgencias  3. Trasladar a Hospitalizacion: Dif Misma Unidad  4. Trasladar a  UCI Adulto: Dif Misma Unidad  5. Trasladar a UCI Pediatrica: Dif Misma Unidad  6. Trasladar a UCI Neonatal: Dif Misma Unidad  7. Trasladar a Consulta Externa: Dif Misma Unidad  8. Trasladar a  Cirugia: Dif Misma Unidad  9. Hospitalizacion en Casa  10. Referencia  11. Morgue  12. Salida  13. Continua en la Unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'INDICAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desgarro:  1: SI  2: NO  3: NO APLICA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCATINPAR', @level2type = N'COLUMN', @level2name = N'PREDESGAR';

