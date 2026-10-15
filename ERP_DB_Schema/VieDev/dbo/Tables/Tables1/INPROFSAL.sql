CREATE TABLE [dbo].[INPROFSAL] (
    [CODPROSAL]                        CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')   NOT NULL,
    [CODIGONIT]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')              NULL,
    [NOMMEDICO]                        CHAR (60) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')             NOT NULL,
    [MEDPRINOM]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')        NOT NULL,
    [MEDSEGNOM]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')       NOT NULL,
    [MEDPRIAPEL]                       CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')     NOT NULL,
    [MEDSEGAPEL]                       CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')    NOT NULL,
    [CODESPEC1]                        CHAR (3)                                                                        NOT NULL,
    [CODESPEC2]                        CHAR (3)                                                                        NULL,
    [CODESPEC3]                        CHAR (3)                                                                        NULL,
    [IMDIRECCI]                        CHAR (100) MASKED WITH (FUNCTION = 'default()')                                 NOT NULL,
    [IMTELEFON]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')            NOT NULL,
    [IMTELMOVI]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "CellPhone_Ofuscado", 0)')        NOT NULL,
    [TARJETAPR]                        CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "ProfessionalCard_Ofuscado", 0)') NULL,
    [ESTADOMED]                        INT                                                                             NOT NULL,
    [MEDTIPVIN]                        INT                                                                             NOT NULL,
    [CODUSUARI]                        CHAR (20)                                                                       NULL,
    [TIPPROFES]                        INT                                                                             NOT NULL,
    [MEDICFOTO]                        VARBINARY (MAX)                                                                 NULL,
    [MEDIFIRMA]                        VARBINARY (MAX)                                                                 NULL,
    [MEDIHUELL]                        VARBINARY (MAX)                                                                 NULL,
    [MEDPERCIR]                        CHAR (1)                                                                        NULL,
    [INDAUDFOR]                        NUMERIC (18)                                                                    NOT NULL,
    [REACONEXT]                        BIT                                                                             NOT NULL,
    [GENCONTRA]                        INT                                                                             NULL,
    [FECULTLIQ]                        DATETIME                                                                        NULL,
    [FECULTLIQTMP]                     DATETIME                                                                        NULL,
    [GENPROVEE]                        INT                                                                             NULL,
    [GENLINDIST]                       INT                                                                             NULL,
    [AUUBICACI]                        CHAR (20)                                                                       NULL,
    [MOSTRARWEB]                       BIT                                                                             NULL,
    [AssistedConsultation]             BIT                                                                             NULL,
    [PerformAssistedInterconsultation] BIT                                                                             NULL,
    [IDADTIPOIDENTIFICA]               INT                                                                             NULL,
    CONSTRAINT [PK_INMEDICOS] PRIMARY KEY CLUSTERED ([CODPROSAL] ASC),
    CONSTRAINT [FK_INPROFSAL_ADTIPOIDENTIFICA_IDADTIPOIDENTIFICA] FOREIGN KEY ([IDADTIPOIDENTIFICA]) REFERENCES [dbo].[ADTIPOIDENTIFICA] ([ID]),
    CONSTRAINT [FK_INPROFSAL_INESPECIA] FOREIGN KEY ([CODESPEC1]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_INPROFSAL_INESPECIA1] FOREIGN KEY ([CODESPEC2]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_INPROFSAL_INESPECIA2] FOREIGN KEY ([CODESPEC3]) REFERENCES [dbo].[INESPECIA] ([CODESPECI])
);


GO
ALTER TABLE [dbo].[INPROFSAL] NOCHECK CONSTRAINT [FK_INPROFSAL_ADTIPOIDENTIFICA_IDADTIPOIDENTIFICA];


GO
ALTER TABLE [dbo].[INPROFSAL] NOCHECK CONSTRAINT [FK_INPROFSAL_INESPECIA];


GO
ALTER TABLE [dbo].[INPROFSAL] NOCHECK CONSTRAINT [FK_INPROFSAL_INESPECIA1];


GO
ALTER TABLE [dbo].[INPROFSAL] NOCHECK CONSTRAINT [FK_INPROFSAL_INESPECIA2];


GO
CREATE NONCLUSTERED INDEX [IX_INPROFSAL_ESTADOMED_CODESPEC1]
    ON [dbo].[INPROFSAL]([ESTADOMED] ASC)
    INCLUDE([CODESPEC1]);


GO
CREATE NONCLUSTERED INDEX [IX_INPROFSAL_ESTADOMED]
    ON [dbo].[INPROFSAL]([ESTADOMED] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tipo de identificacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'IDADTIPOIDENTIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que almacena si el profesional "Realiza interfconsulta asistida".  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'PerformAssistedInterconsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional de la salud que realiza la atención médica en modalidad de consulta asistida :   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'AssistedConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar en la Web:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la linea de distribucion del proveedor, Este campo solo se llena si el contrato que se le asigno es de tipo Estandar     Los campos de proveedores se utilizan en la liquidacion de honorarios medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'GENLINDIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perfil de Cirugia:  0. Ninguno  1. Cirujano  2. Anestesiologos  3. Ayudantes  4. Anestesiologos/ Cirujano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDPERCIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 Medico General  2 Medico Especialista  3 Enfermera  4 Auxiliar Enfermeria  5 Odontologo General  6 Odontologo Especialista  7 Nutricionista  8 Higienista  9 Psicologo  10 Trabajadora Social  11 Promotor de Saneamiento  12 Ingeniero Sanitario  13 Medico Veterinario  14 Ingeniero Alimento  15 Auxiliar Bacteriologo  16 Terapeuta  17 Optometra  18 Quimico Farmaceutico  19 Radiologo  20 Tecnologo Radiologo  21 Instrumentador Qx  22 Auxiliar Patologia  23 Otros  24 Medico Interno  25 Bacteriologo(a)  26 Patólogo(a)  27               Médico residente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'TIPPROFES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Vinculacion  1. Planta  2. Contrato  3. Residente  4. Interno  5. Externo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'MEDTIPVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Profesional de la Salud:  1: Activo  2: Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPROFSAL', @level2type = N'COLUMN', @level2name = N'ESTADOMED';

