CREATE TABLE [dbo].[HCMOANULB] (
    [CODMOTANU] CHAR (4)  NOT NULL,
    [DESMOTANU] CHAR (80) NULL,
    [TIPSERIPS] INT       NULL,
    [ESTADO]    INT       CONSTRAINT [DF_ESTADO] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCMOANULB] PRIMARY KEY CLUSTERED ([CODMOTANU] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del motivo de anulación: 1=Activo, 2=Inactivo. Indica si el motivo está habilitado para usar en anulaciones de servicios RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:   1=Activo  2=Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio RIPS anulable: laboratorios (1), patologías (2), imagenología (3), procedimientos quirúrgicos (5), diálisis (7), egreso RIAS (8), inatención citas (9), falta prescripción MinSalud (10), incapacidad máxima (11), atención domiciliaria (12), autorización duplicada (13), egreso PAD (14), traslados internos (15-16), enfermería (17), devolución imágenes diagnósticas (18-20), reprogramación citas (21), anulación quimio/radioterapia (22), esquemas terapéuticos (23-27), medicamentos (28-30), documentos clínicos (31-32), incapacidades (33-34), procedimientos no QX (36), factor riesgo (37), leche materna (38), terapia renal (39), antecedentes (40).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Laboratorios = 1,    
Patologias=2,    
Imagenologia=3,    
Procedimientos QX=5,    
Diálisis=7,    
Egreso RIAS=8,    
Inatención de Citas=9,    
No registro de # de prescripción MINSALUD=10,    
Justificación para exceder los Días Máximos de Incapacidad=11,    Atención domiciliaria=12,    
Permitir Registrar mismo Número de Autorización=13,    
Egreso PAD"= 14,  
("Solicitud de Traslados Internos=15,    
Cancelar Solicitud traslados internos=16,    
No realizada - Actividad enfermería=17,    
Imagenes Diagnosticas - Devolución Imagen(examen)=18,    
Imagenes Diagnosticas - Segunda opinión=19,    
Imagenes Diagnosticas - Motivo Sobreexposición=20,    
Reprogramar citas=21,    
Anular orden (quimioterapia, radioterapia externa, braquiterapia)=22,    Modificación de Esquemas=23,
Enrutamiento del medicamento = 24,
Devolución mezcla = 25,
Indicaciones sujeciones terapéuticas = 26,
Descontinuar sujeciones terapéuticas = 27,
Anulación de entrega de medicamentos = 28,
Anular componentes lácteos = 29,
Omitir firma electrónica paciente y/o acudiente = 30
Anulación pre-alta hospitalaria = 31,
Anular consentimiento informado = 32,
Anulación de incapacidades y licencias = 33,
Modificación de incapacidades y licencias = 34
Procedimientos No QX = 36
Anulación factor de riesgo = 37
Descarte de almacenamiento de leche materna = 38
Finalización prematura/Anular Terapia de Reemplazo Renal = 39
No registro de antecedentes = 40', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del motivo de anulación aplicable a servicios de salud (laboratorios, procedimientos, atenciones, medicamentos, etc.) en RIPS. Permite clasificar y justificar anulaciones en el sistema de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'DESMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de Anulacion de los laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'DESMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'DESMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único del motivo de anulación (4 caracteres). Clave primaria que referencia motivos de anulación de servicios RIPS, laboratorios, procedimientos quirúrgicos, atenciones, medicamentos y otros servicios de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Motivo de Anulacion de los laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de motivos de anulación de órdenes o servicios en historia clínica. Registra las razones válidas por las cuales se puede anular un servicio, orden médica o procedimiento, clasificadas por tipo de servicio IPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB';
