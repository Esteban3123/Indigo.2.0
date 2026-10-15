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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Traslado de ambulacia -  tipo de ambulaciona :  1 - Basica  2 - Medicada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'AMBTIPOAMBULANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bala portatil  true: si  false: no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIBALAPORTATIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bala domiciliaria  true - SI  false -NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDEN', @level2type = N'COLUMN', @level2name = N'OXIBALADOMICILIARIA';

