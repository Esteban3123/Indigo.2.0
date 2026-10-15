CREATE TABLE [dbo].[HCMOANULB] (
    [CODMOTANU] CHAR (4)  NOT NULL,
    [DESMOTANU] CHAR (80) NULL,
    [TIPSERIPS] INT       NULL,
    [ESTADO]    INT       CONSTRAINT [DF_ESTADO] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCMOANULB] PRIMARY KEY CLUSTERED ([CODMOTANU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:   1=Activo  2=Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Laboratorios = 1,    
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
Anulación factor de riesgo = 37', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de Anulacion de los laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'DESMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Motivo de Anulacion de los laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCMOANULB', @level2type = N'COLUMN', @level2name = N'CODMOTANU';

