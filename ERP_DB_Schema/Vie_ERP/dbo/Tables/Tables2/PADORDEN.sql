CREATE TABLE [dbo].[PADORDEN] (
    [ID]                                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADCONTROL]                      INT             NOT NULL,
    [RESUMENHC]                         VARCHAR (MAX)   NULL,
    [PERTINENCIA]                       VARCHAR (MAX)   NULL,
    [HORAALDIA]                         INT             NULL,
    [DIASALMES]                         INT             NULL,
    [CAMBIOPOSICION]                    BIT             NULL,
    [SUMINISTROALIMENT]                 BIT             NULL,
    [ASEOBANOPACIENTE]                  BIT             NULL,
    [LUBRICACIONPIEL]                   BIT             NULL,
    [ASISTENCIAMOVILIZACION]            BIT             NULL,
    [ADMINISTRACIONENDOVENOSO]          BIT             NULL,
    [ALIMENTACIONPARENTAL]              BIT             NULL,
    [SONDAVESICAL]                      BIT             NULL,
    [VIGILANCIAHERIDAS]                 BIT             NULL,
    [CATETERESPERIFERICOS]              BIT             NULL,
    [OBSERVACIONENFERMERIA]             VARCHAR (MAX)   NULL,
    [VISITAMEDICA_ACTUALIZACIONFORMULA] BIT             NULL,
    [VISITAMEDICA_DIASPORMES]           INT             NULL,
    [VISITAMEDICA_OBSERVACIONES]        VARCHAR (MAX)   NULL,
    [HERIDAS_DIASPORMES]                INT             NULL,
    [HERIDAS_FRECUENCIA]                VARCHAR (200)   NULL,
    [HERIDAS_MOTIVO]                    VARCHAR (MAX)   NULL,
    [HERIDAS_OBSERVACIONES]             VARCHAR (MAX)   NULL,
    [OXILISTROS]                        DECIMAL (18, 2) NULL,
    [OXIHORASDIA]                       INT             NULL,
    [OXIDIASALMES]                      INT             NULL,
    [OXIBALADOMICILIARIA]               BIT             NULL,
    [OXIBALAPORTATIL]                   BIT             NULL,
    [OXICODVIAADM]                      CHAR (3)        NULL,
    [OXIOBSERVACIONES]                  VARCHAR (MAX)   NULL,
    [AMBTIPOAMBULANCIA]                 INT             NULL,
    [AMBTELEFONO]                       VARCHAR (100)   NULL,
    [AMBNOMBRERESPONSABLE]              VARCHAR (200)   NULL,
    [AMBOBSERVACIONES]                  VARCHAR (MAX)   NULL,
    [OBSERVACIONFORMULAMED]             VARCHAR (MAX)   NULL,
    [FECHAREGISTRO]                     DATETIME        NOT NULL,
    [CODUSUARIO]                        CHAR (20)       NOT NULL,
    CONSTRAINT [PK_PHDORDEN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PHDORDEN_PHDCONTROL] FOREIGN KEY ([IDPADCONTROL]) REFERENCES [dbo].[PADCONTROL] ([ID])
);


GO
ALTER TABLE [dbo].[PADORDEN] NOCHECK CONSTRAINT [FK_PHDORDEN_PHDCONTROL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones generales de la formula medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVACIONFORMULAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del traslado de ambulancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBOBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Responsable de traslado de paciente en ambulancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBNOMBRERESPONSABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono contacto de ambulancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBTELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Traslado de ambulacia -  tipo de ambulaciona :  1 - Basica  2 - Medicada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBTIPOAMBULANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones generales de tratamiento de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIOBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo via de administracion de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXICODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bala portatil  true: si  false: no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIBALAPORTATIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bala domiciliaria  true - SI  false -NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIBALADOMICILIARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad de dias al mes de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIDIASALMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Horas al dia de oxigeno ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIHORASDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad de litros de oxigeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXILISTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clinica de Heridas  - Observaciones generales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_OBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clinica de Heridas  - Motivos de solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_MOTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clinica de Heridas  -Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_FRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clinica de Heridas  - Cantidad de dias X mes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HERIDAS_DIASPORMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visita Medica - Observaciones generales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_OBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visita Medica - cuantas visitas por mes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_DIASPORMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visita Medica - Actualizacion de formulas (medicamentos, oxigeno)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VISITAMEDICA_ACTUALIZACIONFORMULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos - Observacion generales de cuidados enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVACIONENFERMERIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos - Manejo de cateteres perifericos y cambio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CATETERESPERIFERICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos - Cuidado y vigilancia de heridas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'VIGILANCIAHERIDAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos - manejo de sonda vesical', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'SONDAVESICAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos -Alimentacion parental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ALIMENTACIONPARENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos - Administtracion de medicamentos endovenosos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ADMINISTRACIONENDOVENOSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos - Asistencia para movilizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ASISTENCIAMOVILIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos - Lubricacion de piel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'LUBRICACIONPIEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos - aseo y baño del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ASEOBANOPACIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos - suministri de alimentacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'SUMINISTROALIMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuidados Basicos - cambios de posicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'CAMBIOPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad de dias al mes de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'DIASALMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'horas al dia de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'HORAALDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'pertinencia para la PHD y plan de manejo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'PERTINENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resumen de HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'RESUMENHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID control de PHD (PHDCONTROL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'ID';

