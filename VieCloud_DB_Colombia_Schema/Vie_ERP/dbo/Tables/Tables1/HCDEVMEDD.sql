CREATE TABLE [dbo].[HCDEVMEDD] (
    [CODCONCEC]                    INT                                                                              NOT NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                    CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPRODUC]                    CHAR (20)                                                                        NOT NULL,
    [CANDEVOLV]                    INT                                                                              NOT NULL,
    [CANPENDIE]                    INT                                                                              NOT NULL,
    [PROESTADO]                    CHAR (1)                                                                         NOT NULL,
    [FECRESGIS]                    DATETIME                                                                         NOT NULL,
    [CODUSUARI]                    CHAR (20)                                                                        NOT NULL,
    [Id]                           INT                                                                              IDENTITY (1, 1) NOT NULL,
    [IdDetailPhysicalCUM]          INT                                                                              NULL,
    [IdHCMOANULB]                  CHAR (4)                                                                         NULL,
    [DevolutionObservations]       VARCHAR (100)                                                                    NULL,
    [IdNursingPackagesOrderDetail] INT                                                                              NULL,
    CONSTRAINT [PK_HCDEVMEDD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__HCDEVMEDD__CODCE__3450119C] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK__HCDEVMEDD__CODPR__36385A0E] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK__HCDEVMEDD__CODUS__3914C6B9] FOREIGN KEY ([CODUSUARI]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK__HCDEVMEDD__IdDet__2E973846] FOREIGN KEY ([IdDetailPhysicalCUM]) REFERENCES [MedicalHistory].[DetailPhysicalCUM] ([Id]),
    CONSTRAINT [FK__HCDEVMEDD__IdHCM__3173A4F1] FOREIGN KEY ([IdHCMOANULB]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK__HCDEVMEDD__IPCOD__307F80B8] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK__HCDEVMEDD__NUMIN__3267C92A] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK__HCDEVMEDD__UFUCO__354435D5] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCDEVMEDD_ MedicalHistory.NursingPackagesOrderDetail] FOREIGN KEY ([IdNursingPackagesOrderDetail]) REFERENCES [MedicalHistory].[NursingPackagesOrderDetail] ([Id])
);


GO
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK__HCDEVMEDD__CODUS__3914C6B9];


GO
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK__HCDEVMEDD__IdDet__2E973846];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCDEVMEDD].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCDEVMEDD].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
CREATE NONCLUSTERED INDEX [IX_HCDEVMEDD_PROESTADO]
    ON [dbo].[HCDEVMEDD]([PROESTADO] ASC)
    INCLUDE([IdDetailPhysicalCUM]);


GO
CREATE NONCLUSTERED INDEX [IX_HCDEVMEDD_CANPENDIE]
    ON [dbo].[HCDEVMEDD]([CANPENDIE] ASC)
    INCLUDE([CODCONCEC], [CODPRODUC]);


GO
CREATE NONCLUSTERED INDEX [IDX_HCDEVMEDD_IPCODPACI_NUMINGRES_CODPRODUC_Includes]
    ON [dbo].[HCDEVMEDD]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODPRODUC] ASC)
    INCLUDE([CODCONCEC], [PROESTADO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la orden de paquetes de enfermería (MedicalHistory.NursingPackagesOrderDetail). Registra el devolutivo originado desde hoja de procedimientos de enfermería, vinculado a atención hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id de la tabla MedicalHistory.NursingPackagesOrder cuando realizan el devolutivo desde hoja de procedimientos de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas textuales (VARCHAR 100) sobre la devolución de medicamentos o productos terminados; principalmente para mezclas preparadas en central de mezclas, justificación de devolutivo parcial o total.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'DevolutionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de la devolucion, para las mezclas en productos terminados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'DevolutionObservations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'DevolutionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del motivo de devolución (CHAR 4, FK a HCMOANULB.CODMOTANU). Clasifica causas de devolutivo: suspenisión médica, cambio de medicación, mezcla rechazada, productos terminados no utilizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de devolucion: para las mezclas en productos terminados    Relacion con la tabla (HCMOANULB) de motivos y casusas generales   Se relaciona con el campo: CODMOTANU  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdHCMOANULB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK a MedicalHistory.DetailPhysicalCUM) del detalle físico en central de mezclas. Vincula devolutivo a preparación de mezclas/productos terminados; medicamentos convencionales no aplican.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdDetailPhysicalCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla: DetailPhysicalCUM, esto para los productos terminados de la central mezclas, por el momento esto aplica para las mezclas, medicamentos sigue tal cual.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdDetailPhysicalCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdDetailPhysicalCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY). Clave primaria, consecutivo único de registro de devolución de medicamentos en la tabla HCDEVMEDD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario (CHAR 20, FK a SEGusuaru). Identificación del profesional o farmacéutico que registra o autoriza la devolución de medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro (DATETIME). Marca temporal de cuándo se registró la devolución del medicamento o producto en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'FECRESGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'FECRESGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'FECRESGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la devolución (CHAR 1): 1=Pendiente de entregar, 2=Entregado/Procesado, 3=Anulado por suspensión médica del producto. Controla flujo de devolutivos parciales y totales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'PROESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la devolucion:  1. Pendiente  2. Entregado  3. Anulado por Suspencion del producto por parte del medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'PROESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'PROESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente (INT). Unidades de medicamento aún por entregar en devolutivos parciales; complementa CANDEVOLV para rastrear entregas incompletas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CANPENDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pendiente de Entregar para el tema de devolutivos parciales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CANPENDIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CANPENDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad devuelta (INT). Número de unidades del medicamento/producto efectivamente retornadas al centro de mezclas o farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CANDEVOLV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Devuelta  del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CANDEVOLV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CANDEVOLV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto (CHAR 20). Identificador del medicamento, mezcla o producto terminado objeto de la devolución; vinculado a catálogo de productos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (CHAR 20, FK a INPROFSAL, PII ofuscado). Médico, enfermero o profesional que prescribió o autorizó el retorno del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10, FK a INUNIFUNC). Departamento, servicio o área clínica donde se originó la devolución (urgencia, hospitalización, quirófano, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10, FK a ADCENATEN). Sede, hospital o clínica donde se registra el devolutivo de medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (CHAR 10, FK a ADINGRESO). Identificador único de la admisión/atención del paciente asociada a la devolución de medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, FK a INPACIENT, PII ofuscado con partial mask). Cédula, identificación o documento del paciente cuyo medicamento se devuelve; equivalente a identificación única del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo (INT). Número secuencial correlativo del registro de devolución dentro del contexto de la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de devoluciones de medicamentos y dispositivos médicos durante un ingreso hospitalario. Controla las cantidades devueltas y pendientes por paciente, profesional de salud y producto, incluyendo el estado del proceso de devolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD';
