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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establecer si la actividad medica, corresponde a transfusión de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'HemocomponentsRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Radioterapia Externa  2 - Braquiterapia  3 - Quimioteapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIPOTRATAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - Actividad tipo Dialisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_DIALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - Actividad medicina general de control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - Actividad medicina general', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Apl. a cons de paquetes de Ttos Especiales y Diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'PAQDIALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración de la actividad variable control  si  no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIEMPOVARCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración de la cita de control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DURAACTIVCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del CUPS de la cita de control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODSERIPSCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Me identifica si la primera vez y control es automatico desde el formulario de Asignación de citas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'PRIMECONTROLAUTOMATICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me relaciona el ID de la RIASCUPS con la actividad de agendamiento, esto solamente se llena cuando la actividad Aplica a RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para identificar si la actividad medica pertenece a una Urgencias Odontologicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'URGENCIASODONTOLOGICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para identificar si la actividad aplica para RIAS, solo se habilita cuando es Cita medica ó apoyo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'APLICARIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Res 256:   0: Ninguno   1:Medicina General   2:Odontología General   3:Medicina Interna   4:Pediatría   5:Ginecología   6:Obstetricía   7:Cirugía Géneral   8:Toma ecografía   9:Toma resonancía magnética nuclear   10:Cirugía de cataratas   11:Cirugía reemplazo de cadera   12:Cirugía de revascularización miocárdica   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIPRES256';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505NOAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(v.73) Consulta de Adulto Primera Vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONADPRV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(v.72) Consulta de Joven Primera Vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONJOPRV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(v.69) Consulta de Crecimiento y Desrrollo Primera Vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONCREDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(v.68) Consulta de Psicología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONPSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(v.67) Consulta de Nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONNUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(v.63) Consulta por Oftalmología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONOFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(v.57) Control Prenatal (No Primera Vez)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505COPRNPRV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(v.56) Control Prenatal de Priemera Vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505COPRPRV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(v.53) Planificación Familiar Primera Vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505PLFAPRV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Actividad (Activo o inactivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'ESTADOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Pacientes Maximo de la Actividad Grupal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMPACMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Pacientes Minimo de la Actividad Grupal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMPACMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actividad de consulta Grupal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CONGRUPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones Medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'INDICAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Citas Mensuales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMCITASM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de citas Mensual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRCITASM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de Citas diarias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMCITASD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de citas Diarias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRCITASD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del intervalo entre citas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMINTERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control para el intervalo de citas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRINTCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo Variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIEMPOVAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion de la Actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DURAACTIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actividad de Consulta  0: Cita médica ó consulta externa  1: Procedimiento Qx  2: Otros Procedimientos (Apoyo Diagnóstico-terapeutico-otros procedimientos)  3: Tratamientos oncológicos ó especiales  4: Diálisis  5: Mixta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'ACTIVICON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la actividad Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DESACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Actividad Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODACTMED';

