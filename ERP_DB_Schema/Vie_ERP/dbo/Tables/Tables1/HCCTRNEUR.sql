CREATE TABLE [dbo].[HCCTRNEUR] (
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST]  DATETIME                                                                         NOT NULL,
    [APEOJOESP]  CHAR (10)                                                                        NULL,
    [APEOJOVOZ]  CHAR (1)                                                                         NULL,
    [APEOJODOL]  CHAR (1)                                                                         NULL,
    [APEOJONIN]  CHAR (1)                                                                         NULL,
    [RESVERORI]  CHAR (1)                                                                         NULL,
    [RESVERCON]  CHAR (1)                                                                         NULL,
    [RESVERINA]  CHAR (1)                                                                         NULL,
    [RESVERINC]  CHAR (1)                                                                         NULL,
    [RESVERNIN]  CHAR (1)                                                                         NULL,
    [RESMOTOBE]  CHAR (1)                                                                         NULL,
    [RESMOTLOC]  CHAR (1)                                                                         NULL,
    [RESMOTRET]  CHAR (1)                                                                         NULL,
    [RESMOTFLE]  CHAR (1)                                                                         NULL,
    [RESMOTEXT]  CHAR (1)                                                                         NULL,
    [RESMOTFLA]  CHAR (1)                                                                         NULL,
    [VALGLASGO]  CHAR (2)                                                                         NULL,
    [PUPDERTAM]  CHAR (1)                                                                         NULL,
    [PUPDERREAC] CHAR (2)                                                                         NULL,
    [PUPIZQTAM]  CHAR (1)                                                                         NULL,
    [PUPIZQREA]  CHAR (2)                                                                         NULL,
    [FMMMSSNOR]  CHAR (1)                                                                         NULL,
    [FMMMSSDEB]  CHAR (1)                                                                         NULL,
    [FMMMSSAUS]  CHAR (1)                                                                         NULL,
    [FMMMIINOR]  CHAR (1)                                                                         NULL,
    [FMMMIIDEB]  CHAR (1)                                                                         NULL,
    [FMMMIIAUS]  CHAR (1)                                                                         NULL,
    [ESTMENALE]  CHAR (1)                                                                         NULL,
    [ESTMENSOM]  CHAR (1)                                                                         NULL,
    [ESTMENEST]  CHAR (1)                                                                         NULL,
    [ESTMENCOM]  CHAR (1)                                                                         NULL,
    [OBSGENREG]  VARCHAR (500)                                                                    NULL,
    [REFNORMAL]  CHAR (1)                                                                         NULL,
    [REFAUMENT]  CHAR (1)                                                                         NULL,
    [REFDISMIN]  CHAR (1)                                                                         NULL,
    [REFBABYNS]  CHAR (1)                                                                         NULL,
    [RESNORMAL]  CHAR (1)                                                                         NULL,
    [RESCHEYNE]  CHAR (1)                                                                         NULL,
    [RESKUSSMA]  CHAR (1)                                                                         NULL,
    [RESPIBIOT]  CHAR (1)                                                                         NULL,
    [RESAPNEUS]  CHAR (1)                                                                         NULL,
    [RESPARRES]  CHAR (1)                                                                         NULL,
    [RESASIRES]  CHAR (1)                                                                         NULL,
    [POSCABPAC]  CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_HCCTRNEUR] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC, [FECREGIST] ASC),
    CONSTRAINT [FK_HCCTRNEUR_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCCTRNEUR_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCCTRNEUR_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCCTRNEUR_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCCTRNEUR_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCCTRNEUR] NOCHECK CONSTRAINT [FK_HCCTRNEUR_ADCENATEN];


GO
ALTER TABLE [dbo].[HCCTRNEUR] NOCHECK CONSTRAINT [FK_HCCTRNEUR_ADINGRESO];


GO
ALTER TABLE [dbo].[HCCTRNEUR] NOCHECK CONSTRAINT [FK_HCCTRNEUR_INPACIENT];


GO
ALTER TABLE [dbo].[HCCTRNEUR] NOCHECK CONSTRAINT [FK_HCCTRNEUR_INPROFSAL];


GO
ALTER TABLE [dbo].[HCCTRNEUR] NOCHECK CONSTRAINT [FK_HCCTRNEUR_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posicion de la Cabecera del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'POSCABPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiracion - Asistencia Respiratoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESASIRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiracion - Paro Respiratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESPARRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiracion - Apneustica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESAPNEUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiracion - Biot', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESPIBIOT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiracion - Kussmaul', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESKUSSMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiracion - Cheynestoke', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESCHEYNE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respiracion - Normal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESNORMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reflejos - Babynsky', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFBABYNS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reflejos - Disminuidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFDISMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reflejos - Aumentados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFAUMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reflejos - Normal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'REFNORMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion General del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'OBSGENREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Mental - Coma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENCOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Mental - Estupor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Mental - Somnolencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENSOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Mental - Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'ESTMENALE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuerza Muscular - MMII Ausente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIIAUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuerza Muscular - MMII Debil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIIDEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuerza Muscular - MMII Normal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMIINOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuerza Muscular - MMSS Ausente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSAUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuerza Muscular - MMSS Debil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSDEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuerza Muscular - MMSS Normal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FMMMSSNOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pupila Izaquierda - Reaccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPIZQREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pupila Izquierda - Tamaño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPIZQTAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pupila Derecha - Reaccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPDERREAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pupila Derecha - Tamaño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'PUPDERTAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Galsgow Calculado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'VALGLASGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Motora - Flacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTFLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Motora - Extension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Motora - Flexion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTFLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Motora - Retira al Dolor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Motora - Localiza Dolor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Motora - Obedece Ordenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESMOTOBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Verbal - Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERNIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Verbal - Ruidos Incomprensibles', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Verbal - Palabras Inapropiadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Verbal - Conversacion Confusa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Respuesta Verbal - Conversacion Orientada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'RESVERORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apertura de Ojos - Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJONIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apertura de Ojos - Al Dolor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJODOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apertura de Ojos - A la Voz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJOVOZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apertura de Ojos - Espontanea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'APEOJOESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCTRNEUR', @level2type = N'COLUMN', @level2name = N'IPCODPACI';

