CREATE TABLE [dbo].[HCNOSERFD] (
    [ID]                                 INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]                          INT            NOT NULL,
    [CODPRODUC]                          CHAR (20)      NOT NULL,
    [OBSERVACI]                          VARCHAR (MAX)  NULL,
    [IDMEZCLAS]                          NUMERIC (18)   NULL,
    [TIPO]                               INT            NULL,
    [AppropriateTreatment]               BIT            NULL,
    [IdSourceTable]                      INT            NULL,
    [SourceTable]                        VARCHAR (50)   NULL,
    [MedicalObservation]                 VARCHAR (1000) NULL,
    [State]                              INT            NULL,
    [CurrentMedication]                  INT            NULL,
    [ConfirmAppropriateTreatment]        BIT            NULL,
    [ObservationConfirm]                 VARCHAR (200)  NULL,
    [ProfessionalCodeMedicalObservation] CHAR (20)      NULL,
    [ProfessionalCode]                   CHAR (20)      NULL,
    [DateRegistration]                   DATETIME       NULL,
    CONSTRAINT [PK_HCNOSERFD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNOSERFD_HCINFLIQC] FOREIGN KEY ([IDMEZCLAS]) REFERENCES [dbo].[HCINFLIQC] ([CODCONCEC]),
    CONSTRAINT [FK_HCNOSERFD_HCNOSERFA] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCNOSERFA] ([AUTO]),
    CONSTRAINT [FK_HCNOSERFD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCNOSERFD] NOCHECK CONSTRAINT [FK_HCNOSERFD_HCINFLIQC];


GO
ALTER TABLE [dbo].[HCNOSERFD] NOCHECK CONSTRAINT [FK_HCNOSERFD_HCNOSERFA];


GO
ALTER TABLE [dbo].[HCNOSERFD] NOCHECK CONSTRAINT [FK_HCNOSERFD_IHLISTPRO];


GO
CREATE NONCLUSTERED INDEX [IDX_HCNOSERFD_CODPRODUC]
    ON [dbo].[HCNOSERFD]([CODPRODUC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que diligencia la observación medica del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeMedicalObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observacion de la confirmacion del tratamiento idoneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ObservationConfirm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirmar tratamiento idóneo: 0=No 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ConfirmAppropriateTreatment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo para validar los medicamentos activos, se insertan nuevos registros al guardar la nota. 

0 -> INACTIVO  (medicamento NO vigente)
1 -> ACTIVO (medicamento vigente)

Nota: campo de vital importancia para que en el plan manejo no se duplique y se maneje correctamente la alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CurrentMedication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - Validado por el farmaceutico
2 - Validado por el medico
4 - Modificado por el medico
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obervacion medica realizada desde la orden en la HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'MedicalObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de las tablas origen: HCPRESCRA - HCNOSERFOTROMED', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'SourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de las tablas origen: HCPRESCRA - HCNOSERFOTROMED', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'IdSourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tratamiento idóneo: 0=No 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'AppropriateTreatment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO :   1 - Medicamentos    2 - Mezclas y Liquidos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo ID de la cabecera de las Mezclas y Liquidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'IDMEZCLAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones farmaceuticas realizadas desde la nota de atencion farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del producto ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del consecutivo de la atencion farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modulo Historias Clinicas - Tabla detalle Notas de servicio farmaceutico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD';

