CREATE TABLE [dbo].[AGACTIMED] (
    [CODACTMED]                         CHAR (3)       NOT NULL,
    [DESACTMED]                         NVARCHAR (150) NOT NULL,
    [ACTIVICON]                         CHAR (1)       NOT NULL,
    [DURAACTIV]                         CHAR (3)       NULL,
    [TIEMPOVAR]                         BIT            NULL,
    [CTRINTCIT]                         BIT            NULL,
    [NUMINTERV]                         CHAR (2)       NULL,
    [CTRCITASD]                         BIT            NULL,
    [NUMCITASD]                         CHAR (2)       NULL,
    [CTRCITASM]                         BIT            NULL,
    [NUMCITASM]                         CHAR (2)       NULL,
    [INDICAMED]                         NVARCHAR (250) NULL,
    [CONGRUPAL]                         BIT            NULL,
    [NUMPACMIN]                         CHAR (2)       NULL,
    [NUMPACMAX]                         CHAR (2)       NULL,
    [ESTADOACT]                         BIT            NOT NULL,
    [CODSERIPS]                         CHAR (20)      NULL,
    [R4505PLFAPRV]                      BIT            NULL,
    [R4505COPRPRV]                      BIT            NULL,
    [R4505COPRNPRV]                     BIT            NULL,
    [R4505CONOFT]                       BIT            NULL,
    [R4505CONNUT]                       BIT            NULL,
    [R4505CONPSI]                       BIT            NULL,
    [R4505CONCREDES]                    BIT            NULL,
    [R4505CONJOPRV]                     BIT            NULL,
    [R4505CONADPRV]                     BIT            NULL,
    [R4505NOAPL]                        BIT            CONSTRAINT [DF_AGACTIMED_R4505NOAPL] DEFAULT ((1)) NULL,
    [TIPRES256]                         TINYINT        NULL,
    [APLICARIAS]                        BIT            NULL,
    [URGENCIASODONTOLOGICA]             BIT            NULL,
    [IDRIASCUPS]                        INT            NULL,
    [PRIMECONTROLAUTOMATICO]            BIT            NULL,
    [CODSERIPSCONTROL]                  CHAR (20)      NULL,
    [DURAACTIVCONTROL]                  CHAR (3)       NULL,
    [TIEMPOVARCONTROL]                  BIT            NULL,
    [PAQDIALISIS]                       BIT            NULL,
    [MOSTRARWEB]                        BIT            NULL,
    [IDDESCRIPCIONRELACIONADA_CITA]     INT            NULL,
    [IDDESCRIPCIONRELACIONADA_CONTROL]  INT            NULL,
    [IDDESCRIPCIONRELACIONADA_DIALISIS] INT            NULL,
    [TIPOTRATAMIENTO]                   INT            NULL,
    [HemocomponentsRequest]             BIT            NULL,
    CONSTRAINT [PK_AGACTIMED] PRIMARY KEY CLUSTERED ([CODACTMED] ASC),
    CONSTRAINT [FK_AGACTIMED_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AGACTIMED_RIAS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID])
);


GO
ALTER TABLE [dbo].[AGACTIMED] NOCHECK CONSTRAINT [FK_AGACTIMED_INCUPSIPS];


GO
ALTER TABLE [dbo].[AGACTIMED] NOCHECK CONSTRAINT [FK_AGACTIMED_RIAS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Radioterapia Externa  2 - Braquiterapia  3 - Quimioteapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIPOTRATAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración de la actividad variable control  si  no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIEMPOVARCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Res 256:   0: Ninguno   1:Medicina General   2:Odontología General   3:Medicina Interna   4:Pediatría   5:Ginecología   6:Obstetricía   7:Cirugía Géneral   8:Toma ecografía   9:Toma resonancía magnética nuclear   10:Cirugía de cataratas   11:Cirugía reemplazo de cadera   12:Cirugía de revascularización miocárdica   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIPRES256';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actividad de Consulta  0: Cita médica ó consulta externa  1: Procedimiento Qx  2: Otros Procedimientos (Apoyo Diagnóstico-terapeutico-otros procedimientos)  3: Tratamientos oncológicos ó especiales  4: Diálisis  5: Mixta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'ACTIVICON';

