CREATE TABLE [dbo].[HCURGINI1] (
    [IDETIPHIS]    CHAR (9)                                                                          NOT NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NOT NULL,
    [NUMEFOLIO]    CHAR (10)                                                                         NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                         NOT NULL,
    [CODCENATE]    CHAR (10)                                                                         NOT NULL,
    [UFUCODIGO]    CHAR (10)                                                                         NOT NULL,
    [FECINIATE]    DATETIME                                                                          NOT NULL,
    [MOTCONSUL]    VARCHAR (MAX)                                                                     NOT NULL,
    [ENFACTUAL]    VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "CurrentIllness_Ofuscado", 0)') NOT NULL,
    [REVSISTEMA]   VARCHAR (MAX)                                                                     NULL,
    [ANALISISP]    VARCHAR (MAX)                                                                     NULL,
    [INDICAPAC]    CHAR (2)                                                                          NOT NULL,
    [INDICAMED]    VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Instruction_Ofuscado", 0)')    NOT NULL,
    [TRAINTUNI]    BIT                                                                               NOT NULL,
    [MOTTRAINT]    VARCHAR (2000)                                                                    NULL,
    [TIPATEPAC]    CHAR (1)                                                                          NULL,
    [TIPCITMED]    CHAR (1)                                                                          NULL,
    [INDAUDFOR]    NUMERIC (18)                                                                      NOT NULL,
    [MOSEPICRI]    BIT                                                                               NULL,
    [CODESPTRA]    CHAR (3)                                                                          NULL,
    [FECHINIHI]    DATETIME                                                                          NULL,
    [FECHFINH]     DATETIME                                                                          NULL,
    [PLAINDMED]    VARCHAR (MAX)                                                                     NULL,
    [HCIRENAL]     INT                                                                               NULL,
    [HCIRENALPERI] INT                                                                               NULL,
    CONSTRAINT [PK_HCURGINI1] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCURGINI1_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCURGINI1_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCURGINI1_INESPECIA] FOREIGN KEY ([CODESPTRA]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCURGINI1_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCURGINI1_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCURGINI1_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCURGINI1] NOCHECK CONSTRAINT [FK_HCURGINI1_ADcenaten];


GO
ALTER TABLE [dbo].[HCURGINI1] NOCHECK CONSTRAINT [FK_HCURGINI1_ADINGRESO];


GO
ALTER TABLE [dbo].[HCURGINI1] NOCHECK CONSTRAINT [FK_HCURGINI1_INESPECIA];


GO
ALTER TABLE [dbo].[HCURGINI1] NOCHECK CONSTRAINT [FK_HCURGINI1_INPacient];


GO
ALTER TABLE [dbo].[HCURGINI1] NOCHECK CONSTRAINT [FK_HCURGINI1_INPROFSAL];


GO
ALTER TABLE [dbo].[HCURGINI1] NOCHECK CONSTRAINT [FK_HCURGINI1_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HCI Renal - Unidad Renal  Campo que se registra cuando el HCI sea de tipo Peritoneal.  1 -APD Diálisis Peritoneal Manual  2 - CAPD Diálisis Peritoneal Automatizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HCI Renal - Unidad Renal  1 - Predialisis  2 - Peritoneal  3 - Hemodiálisis ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'HCIRENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Historias Clinicas: Codigo de la Especialidad Tratante  Opcion valida solo para unidades funcionales de tipo Hospitalizacion - Se actualiza con Interconsultas. Muestra la especialidad acutal tratante, no se tiene opcion para guardar historico de especialidades tratantes, opcionalmente se puede obtener de HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'CODESPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Cita Medica:  1. Primera Vez  2. Control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TIPCITMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Atencion:  1. Ginecologica  2. Obstetrica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TIPATEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si se ha realizado un Traslado Interno a otra Unidad, Cuando en las Indicaciones Medicas la enumeracion es 3 o 4.  True: Traslado Interno  False: Traslado a otra IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define el destino del paciente  1. Trasladar a Urgencias: Solo consulta externa  2. Trasladar a Observacion Urgencias: solo Urgencias  3. Trasladar a Hospitalizacion: Dif Misma Unidad  4. Trasladar a  UCI Adulto: Dif Misma Unidad  5. Trasladar a UCI Pediatrica: Dif Misma Unidad  6. Trasladar a UCI Neonatal: Dif Misma Unidad  7. Trasladar a Consulta Externa: Dif Misma Unidad  8. Trasladar a  Cirugia: Dif Misma Unidad  9. Hospitalizacion en Casa  10. Referencia  11. Morgue  12. Salida  13. Continua en la Unidad  15. Retiro Voluntario  16. Fuga', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCURGINI1', @level2type = N'COLUMN', @level2name = N'INDICAPAC';

