CREATE TABLE [dbo].[HCINTERNO] (
    [IDETIPHIS]  CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]  NCHAR (10)                                                                       NOT NULL,
    [CONSFOLIO]  CHAR (10)                                                                        NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHISPAC]  DATETIME                                                                         NOT NULL,
    [TIPHISPAC]  CHAR (2)                                                                         NOT NULL,
    [DIAGNOSTI]  VARCHAR (MAX)                                                                    NULL,
    [ANTECEDE]   VARCHAR (MAX)                                                                    NULL,
    [SUBJETIVO]  VARCHAR (MAX)                                                                    NULL,
    [EXAMENFI]   VARCHAR (MAX)                                                                    NULL,
    [PARACLINI]  VARCHAR (MAX)                                                                    NULL,
    [ANALISIS]   VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')      NULL,
    [DIETAS]     VARCHAR (MAX)                                                                    NULL,
    [MEDICAMEN]  VARCHAR (MAX)                                                                    NULL,
    [MEZYLIQUI]  VARCHAR (MAX)                                                                    NULL,
    [INSUYDISPO] VARCHAR (MAX)                                                                    NULL,
    [LABORATOR]  VARCHAR (MAX)                                                                    NULL,
    [PATOLOGIA]  VARCHAR (MAX)                                                                    NULL,
    [IMAGENES]   VARCHAR (MAX)                                                                    NULL,
    [PROCEDIQX]  VARCHAR (MAX)                                                                    NULL,
    [PROCENOQX]  VARCHAR (MAX)                                                                    NULL,
    [INTERCONSU] VARCHAR (MAX)                                                                    NULL,
    [RECOMENDA]  VARCHAR (MAX)                                                                    NULL,
    [CALFINTER]  DECIMAL (18, 1)                                                                  NULL,
    [CODESPECI]  CHAR (3)                                                                         NULL,
    [NUMEFOIN]   NCHAR (10)                                                                       NULL,
    CONSTRAINT [PK_HCINTERNO_1] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC),
    CONSTRAINT [FK_HCINTERNO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINTERNO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCINTERNO] NOCHECK CONSTRAINT [FK_HCINTERNO_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINTERNO] NOCHECK CONSTRAINT [FK_HCINTERNO_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Folio generado por el estudiante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMEFOIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Calificacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CALFINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para las recomendaciones por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'RECOMENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para las interconsultas por el interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'INTERCONSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para los procedimientos NOQX por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PROCENOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para las procedimientos QX por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PROCEDIQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para las imagenes por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IMAGENES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para las patologias por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PATOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para los laboratorios por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'LABORATOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones para los insumos y dispositivos por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'INSUYDISPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para la mezcla y liquidos por los especialistas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'MEZYLIQUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para los medicamentos por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'MEDICAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para las dietas por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'DIETAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones para el analisis por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'ANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para los paraclinicos por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'PARACLINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para el examne fisico por el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'EXAMENFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para el subjetivo de los especialistas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'SUBJETIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones de los antecedentes para el especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'ANTECEDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones para las observaciones del especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'DIAGNOSTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si la historia es de Ingreso o Evolucion  I: Ingreso  E: Evolucion  O: Otros modelos de apoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de Evoluciones de la Historia Clinica de Ingreso del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTERNO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';

