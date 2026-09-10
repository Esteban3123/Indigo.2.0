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
ALTER TABLE [dbo].[HCNOSERFD] NOCHECK CONSTRAINT [FK_HCNOSERFD_IHLISTPRO];




GO



GO



GO
ALTER TABLE [dbo].[HCNOSERFD] NOCHECK CONSTRAINT [FK_HCNOSERFD_IHLISTPRO];




GO



GO



GO
ALTER TABLE [dbo].[HCNOSERFD] NOCHECK CONSTRAINT [FK_HCNOSERFD_IHLISTPRO];


GO
CREATE NONCLUSTERED INDEX [IDX_HCNOSERFD_CODPRODUC]
    ON [dbo].[HCNOSERFD]([CODPRODUC] ASC);


GO
ALTER INDEX [IDX_HCNOSERFD_CODPRODUC]
    ON [dbo].[HCNOSERFD] DISABLE;

GO

CREATE NONCLUSTERED INDEX IX_HCNOSERFD_CodConcec_CodProducto
ON dbo.HCNOSERFD
(
    CODCONCEC,
    CODPRODUC
)
INCLUDE
(
    OBSERVACI,
    ID
);

GO

CREATE NONCLUSTERED INDEX IX_HCNOSERFD_CurrentMedication_Source
ON dbo.HCNOSERFD
(
    IdSourceTable
)
INCLUDE
(
    CODCONCEC,
    OBSERVACI,
    AppropriateTreatment,
    ConfirmAppropriateTreatment,
    ObservationConfirm,
    CODPRODUC,
    MedicalObservation,
    State,
    ID
)
WHERE CurrentMedication = 1;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (CHAR 20) del profesional de la salud que diligencia/registra la observación médica del medicamento. Referencia al profesional responsable de la anotación clínica sobre el fármaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeMedicalObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional que diligencia la observación medica del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeMedicalObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeMedicalObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación textual (VARCHAR 200) de la confirmación del tratamiento idóneo. Comentarios del profesional validando la adecuación terapéutica del medicamento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ObservationConfirm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de la confirmacion del tratamiento idoneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ObservationConfirm';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ObservationConfirm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bit (0/1) que confirma si el tratamiento es idóneo/adecuado: 0=No apropiado, 1=Sí apropiado. Validación clínica de la pertinencia farmacoterapéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ConfirmAppropriateTreatment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Confirmar tratamiento idóneo: 0=No 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ConfirmAppropriateTreatment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ConfirmAppropriateTreatment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de vigencia del medicamento (INT 0/1): 0=INACTIVO (medicamento NO vigente), 1=ACTIVO (medicamento vigente). Vital para evitar duplicación en plan de manejo y generar alertas de fármacos activos en la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CurrentMedication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para validar los medicamentos activos, se insertan nuevos registros al guardar la nota. 

0 -> INACTIVO  (medicamento NO vigente)
1 -> ACTIVO (medicamento vigente)

Nota: campo de vital importancia para que en el plan manejo no se duplique y se maneje correctamente la alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CurrentMedication';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CurrentMedication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de validación del registro farmacéutico (INT): 1=Validado por farmacéutico, 2=Validado por médico, 4=Modificado por médico. Trazabilidad de revisiones en la nota de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Validado por el farmaceutico
2 - Validado por el medico
4 - Modificado por el medico
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación médica clínica (VARCHAR 1000) realizada desde la orden en la historia clínica. Notas del profesional respecto al medicamento o tratamiento prescrito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'MedicalObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obervacion medica realizada desde la orden en la HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'MedicalObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'MedicalObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la tabla origen (VARCHAR 50): HCPRESCRA (prescripción médica) o HCNOSERFOTROMED (nota de servicio de otros médicos). Identifica la fuente del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'SourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de las tablas origen: HCPRESCRA - HCNOSERFOTROMED', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'SourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'SourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del registro origen proveniente de tablas HCPRESCRA o HCNOSERFOTROMED. Referencia para trazabilidad de la orden farmacéutica original.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'IdSourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las tablas origen: HCPRESCRA - HCNOSERFOTROMED', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'IdSourceTable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'IdSourceTable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bit (0/1) indicador de tratamiento idóneo/adecuado: 0=No es tratamiento idóneo, 1=Sí es tratamiento idóneo. Evaluación farmacéutica de la pertinencia terapéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'AppropriateTreatment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tratamiento idóneo: 0=No 1=Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'AppropriateTreatment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'AppropriateTreatment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prescripción farmacéutica (INT): 1=Medicamentos individuales, 2=Mezclas y Líquidos. Clasificación del producto dispensado en la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TIPO :   1 - Medicamentos    2 - Mezclas y Liquidos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'TIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (NUMERIC 18) de la cabecera/registro maestro de Mezclas y Líquidos (FK a HCINFLIQC). Aplica cuando TIPO=2 para agrupar componentes de preparados farmacéuticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'IDMEZCLAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo ID de la cabecera de las Mezclas y Liquidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'IDMEZCLAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'IDMEZCLAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones farmacéuticas extensas (VARCHAR MAX) realizadas desde la nota de atención farmacéutica. Anotaciones clínicas, recomendaciones, alergias, interacciones detectadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones farmaceuticas realizadas desde la nota de atencion farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto farmacéutico (CHAR 20, FK a IHLISTPRO). Identificador único del medicamento, mezcla o insumo dispensado en la atención farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo (INT) de la atención farmacéutica (FK a HCNOSERFA). Agrupa los detalles de medicamentos/mezclas de una misma nota de servicio farmacéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del consecutivo de la atencion farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico/identificador único (INT IDENTITY) de la tabla HCNOSERFD. Clave primaria que identifica cada detalle de medicamento en nota de servicio farmacéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Módulo Historias Clínicas - Tabla detalle de Notas de Servicio Farmacéutico. Registra observaciones, validaciones y confirmaciones de tratamientos farmacológicos en atenciones de farmacoterapia, incluyendo medicamentos activos, mezclas y líquidos con seguimiento de estados de validación (farmacéutico, médico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modulo Historias Clinicas - Tabla detalle Notas de servicio farmaceutico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que registró o gestionó el registro de la mezcla o producto no seriado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'ProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó el registro del item en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'DateRegistration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFD', @level2type = N'COLUMN', @level2name = N'DateRegistration';
