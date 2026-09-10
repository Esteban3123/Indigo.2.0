CREATE TABLE [dbo].[CHCAMASHO] (
    [CODICAMAS]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMCAMHOS]          CHAR (10)    NOT NULL,
    [DESCCAMAS]          CHAR (50)    NOT NULL,
    [CODCENATE]          CHAR (10)    NOT NULL,
    [UFUCODIGO]          CHAR (10)    NOT NULL,
    [CODCLAHAB]          INT          NOT NULL,
    [CODCLACAM]          INT          NOT NULL,
    [ESTADCAMA]          INT          NOT NULL,
    [CODAISLAM]          INT          NULL,
    [CAMVIRTUA]          BIT          NOT NULL,
    [CAMTRACIR]          BIT          NOT NULL,
    [CAMTRAMED]          BIT          NOT NULL,
    [CAMDEVMED]          BIT          NOT NULL,
    [CAMTIPANO]          BIT          NOT NULL,
    [CODCONCEC]          NUMERIC (18) NULL,
    [CAMRECMED]          BIT          NOT NULL,
    [CAMRECDEV]          BIT          NOT NULL,
    [BedType]            INT          NULL,
    [TypeTransfer]       INT          NULL,
    [ConcurrencyControl] ROWVERSION   NULL,
    CONSTRAINT [PK_CHCAMASHO_1] PRIMARY KEY CLUSTERED ([CODICAMAS] ASC),
    CONSTRAINT [FK_CHCAMASHO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]) ON UPDATE CASCADE,
    CONSTRAINT [FK_CHCAMASHO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]) ON UPDATE CASCADE
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [_dta_index_CHCAMASHO_7_26483173__K8_K5_K4_K1_K3]
    ON [dbo].[CHCAMASHO]([ESTADCAMA] ASC, [UFUCODIGO] ASC, [CODCENATE] ASC, [CODICAMAS] ASC, [DESCCAMAS] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_CHCAMASHO_6_26483173__K8_K3_K1]
    ON [dbo].[CHCAMASHO]([ESTADCAMA] ASC, [DESCCAMAS] ASC, [CODICAMAS] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CHCAMASHO__NUMCAMHOS]
    ON [dbo].[CHCAMASHO]([NUMCAMHOS] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_CHCAMASHO_6_26483173__K1_K5]
    ON [dbo].[CHCAMASHO]([CODICAMAS] ASC, [UFUCODIGO] ASC);


GO
CREATE TRIGGER trg_UpdateCHCAMASHO
ON CHCAMASHO
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE CHCAMASHO
    SET BedType = NULL, TypeTransfer = NULL
    WHERE ESTADCAMA = '1' 
    AND (TypeTransfer IS NOT NULL OR BedType IS NOT NULL);
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de control de concurrencia (SQL Server TIMESTAMP); mecanismo de sincronización para validar traslados simultáneos de camas y evitar conflictos de actualización en operaciones paralelas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'ConcurrencyControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de concurrencia que se utiliza para validar cuando se están realizando traslados de camas de manera simultánea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'ConcurrencyControl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'ConcurrencyControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de traslado del paciente (1:con devolutivos, 2:con medicamentos, 3:ninguno); clasificación que especifica qué activos farmacéuticos acompañan el movimiento y debe resetearse a NULL tras completar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'TypeTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de traslado: Columna que me especifica cuando se realiza un traslado al paciente de que tipo fue dicho traslado:

1 - Traslado con devolutivos
2 - Traslado con medicamentos
3 - Ninguno

Columna que se debe resetear a NULL 

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'TypeTransfer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'TypeTransfer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de cama en doble estancia (1:origen, 2:destino); identificador que marca la cama como punto de partida o destino cuando traslado sin medicamentos crea ocupación simultánea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'BedType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para identificar si la cama es de origen o de destino cuando se presenta la doble estancia al hacer traslado con la opción ninguno: 
 
1 - Cama Origen
2 - Cama Destino
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'BedType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'BedType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de rechazo de devolutivo de medicamentos; bandera booleana que especifica si la cama rechaza devolver fármacos no consumidos al área de farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMRECDEV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la cama tiene rechazo de devolutivos de farmacia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMRECDEV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMRECDEV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de rechazo de traslado de medicamentos; bandera booleana que especifica si la cama rechaza recepcionar medicamentos durante operaciones de traslado del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMRECMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la cama tiene rechazo de traslado de medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMRECMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMRECMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo único del traslado de medicamentos entre unidades; contador secuencial que vincula movimientos de fármacos asociados a la cama para auditoria y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del traslado de medicamentos entre unidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de anonimato; bandera booleana que identifica si la cama requiere confidencialidad total del paciente en registros clínicos y administrativos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMTIPANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la cama posee anonimato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMTIPANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMTIPANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de devolutivo de medicamentos; bandera booleana que especifica si existen medicamentos no consumidos pendientes de devolución a farmacia desde esta cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMDEVMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si hay un Devolutivo de medicamentos de medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMDEVMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMDEVMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de traslado de medicamentos activo; bandera booleana que marca si hay movimiento de medicamentos asociado a esta cama durante traslado del paciente entre unidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMTRAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si hay un traslado de medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMTRAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMTRAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de retorno a unidad de origen post-cirugía; bandera booleana que especifica si el paciente tras procedimiento quirúrgico retorna automáticamente a la cama de procedencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMTRACIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el paciente retorna a la unidad de origen cuando el traslado corresponde a Cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMTRACIR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMTRACIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de cama virtual; bandera booleana que identifica si la cama es de registro lógico (no física) para fines de facturación, auditoria o cobro especial en atención sin ocupación física.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMVIRTUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la cama corresponde a una cama virtual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMVIRTUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CAMVIRTUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de aislamiento epidemiológico (1:aerosol, 2:contacto, 3:estándar, 4:gota, 5:protector); categoría que define protocolos de bioseguridad y prevención de transmisión de infecciones para el paciente internado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODAISLAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Aislamiento:  1: Aerosol  2: Contacto  3: Estandar  4: Gota  5: Protector', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODAISLAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODAISLAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado operativo de la cama (libre, asignada, inactiva, mantenimiento, aislamiento, reservada); indicador que controla disponibilidad para asignar nuevos ingresos, traslados y atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'ESTADCAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Cama: 
1: Libre 
2: Asignada
3: Inactiva  
4: En Mantenimiento  
5: En Aislamiento  
6: Reservada sin Confirmar  
7: Reservada Confirmada
8: Asignada con reserva

 Nota: El estado 1, y 6 permite asignar la habitacion, los demas estados evitaran que la habitacion este disponible. El estado 6 no se registra en esta tabla solo se describe a nivel informativo [Valor no valido].', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'ESTADCAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'ESTADCAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de cama para liquidación de estancia; tipo que determina cómo se cobra la permanencia: urgencias, recuperación post-quirúrgica, hospitalaria, o cuna de observación neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCLACAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de Cama:  1: Observacion Urgencias  2: Recuperacion Post-Quirurgico  3: Hospitalaria  4:Cuna de Observación    Nota: Este campo sirve para identificar como se va a realizar el cobro de la estancia, ejemplo si la cama es de recuperacion esta no liquidara nada, si es de urgencias tendra en cuenta el parametro del Plan de Beneficios para liquidar estancia o si es Hospitalaria liquidara normal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCLACAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCLACAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de habitación (sala, suite, UCI, etc.); tipo de acomodación que define capacidad de camas (1 a 4), privacidad y nivel de atención clínica para el ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCLAHAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de la habitacion:  1: Sala de Observacion  2: Sala de Procedimientos  3: Sala de Recuperacion  4: Habitacion 1 Cama  5: Habitacion 2 Camas  6: Habitacion 3 Camas  7: Habitacion 4 Camas  8: Suite  9: Habitacion Especial  10: UCI  11: Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCLAHAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCLAHAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (FK); identificador de la unidad operativa, servicio o piso (urgencias, UCI, hospitalización, cirugía) donde está ubicada la cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (FK); identificador de la institución o sede hospitalaria donde se ubica la cama; referencia para facturación RIPS y contratación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la cama; nombre o denominación adicional que contextualiza ubicación, características especiales o categoría de la cama dentro de la unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'DESCCAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'DESCCAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'DESCCAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de cama visible en la unidad; identificación humanamente legible de la cama dentro de la habitación, usado para registro en historias clínicas y comunicación clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Numero de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'NUMCAMHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la cama hospitalaria; código numérico de referencia para operaciones de internación, traslado y asignación de camas en el centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maestro de camas hospitalarias. Registra cada cama disponible en el hospital con su ubicación (centro de atención y unidad funcional), clasificación, estado actual y configuraciones especiales como aislamiento, camas virtuales, cirugía, medicina interna o devolución de medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASHO';
