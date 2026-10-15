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
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK__HCDEVMEDD__CODCE__3450119C];


GO
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK__HCDEVMEDD__CODPR__36385A0E];


GO
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK__HCDEVMEDD__CODUS__3914C6B9];


GO
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK__HCDEVMEDD__IdDet__2E973846];


GO
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK__HCDEVMEDD__IdHCM__3173A4F1];


GO
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK__HCDEVMEDD__IPCOD__307F80B8];


GO
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK__HCDEVMEDD__NUMIN__3267C92A];


GO
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK__HCDEVMEDD__UFUCO__354435D5];


GO
ALTER TABLE [dbo].[HCDEVMEDD] NOCHECK CONSTRAINT [FK_HCDEVMEDD_ MedicalHistory.NursingPackagesOrderDetail];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el id de la tabla MedicalHistory.NursingPackagesOrder cuando realizan el devolutivo desde hoja de procedimientos de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de la devolucion, para las mezclas en productos terminados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'DevolutionObservations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de devolucion: para las mezclas en productos terminados    Relacion con la tabla (HCMOANULB) de motivos y casusas generales   Se relaciona con el campo: CODMOTANU  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla: DetailPhysicalCUM, esto para los productos terminados de la central mezclas, por el momento esto aplica para las mezclas, medicamentos sigue tal cual.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IdDetailPhysicalCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'FECRESGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la devolucion:  1. Pendiente  2. Entregado  3. Anulado por Suspencion del producto por parte del medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'PROESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pendiente de Entregar para el tema de devolutivos parciales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CANPENDIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Devuelta  del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CANDEVOLV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDEVMEDD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

