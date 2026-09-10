CREATE TABLE dbo.ImplantCardRegistration
(	
Id                  INT IDENTITY(1,1)  NOT NULL,
IPCODPACI           VARCHAR(25)        NOT NULL,
ImplantationDate    DATETIME           NOT NULL,
ProductName         VARCHAR(400)       NOT NULL,
Model               VARCHAR(400)       NOT NULL,
LotNumber           VARCHAR(400)       NOT NULL,
SerialNumber        VARCHAR(400)       NOT NULL,
Manufacturer        VARCHAR(400)       NOT NULL,
ManufacturerAddress VARCHAR(400)       NOT NULL,
CODCENATE           CHAR(10)               NULL,
UFUCODIGO           CHAR(10)               NULL,
CODPROSALCREATE     CHAR(20)               NULL,
CreationDate        DATETIME           NOT NULL,
Status              BIT                NOT NULL,
CODPROSALDEL        CHAR(20)               NULL,
DeletionDate        DATETIME               NULL,
  
   CONSTRAINT PK_ImplantReg PRIMARY KEY CLUSTERED 
(
	Id ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE dbo.ImplantCardRegistration  WITH CHECK ADD  CONSTRAINT fk_implant_adcenaten FOREIGN KEY(CODCENATE)
REFERENCES dbo.ADCENATEN (CODCENATE)
GO

ALTER TABLE dbo.ImplantCardRegistration CHECK CONSTRAINT fk_implant_adcenaten
GO

ALTER TABLE dbo.ImplantCardRegistration  WITH CHECK ADD  CONSTRAINT fk_implant_codprosaldel FOREIGN KEY(CODPROSALDEL)
REFERENCES dbo.INPROFSAL (CODPROSAL)
GO

ALTER TABLE dbo.ImplantCardRegistration CHECK CONSTRAINT fk_implant_deluser_usuario
GO

ALTER TABLE dbo.ImplantCardRegistration  WITH CHECK ADD  CONSTRAINT fk_implant_inpacient FOREIGN KEY(IPCODPACI)
REFERENCES dbo.INPACIENT (IPCODPACI)
GO

ALTER TABLE dbo.ImplantCardRegistration CHECK CONSTRAINT fk_implant_inpacient
GO

ALTER TABLE dbo.ImplantCardRegistration  WITH CHECK ADD  CONSTRAINT fk_implant_inprofsal FOREIGN KEY(CODPROSALCREATE)
REFERENCES dbo.INPROFSAL (CODPROSAL)
GO

ALTER TABLE dbo.ImplantCardRegistration CHECK CONSTRAINT fk_implant_inprofsal
GO

ALTER TABLE dbo.ImplantCardRegistration  WITH CHECK ADD  CONSTRAINT fk_implant_inunifunc FOREIGN KEY(UFUCODIGO)
REFERENCES dbo.INUNIFUNC (UFUCODIGO)
GO

ALTER TABLE dbo.ImplantCardRegistration CHECK CONSTRAINT fk_implant_inunifunc
GO
--    "Extended Properties" de la tabla y de cada columna)
EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Identificador único del registro de implante (consecutivo autonumérico).' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Código del paciente al que se le implantó el dispositivo/producto (referencia a ADPACIENTE.IPCODPACI).' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'IPCODPACI'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Fecha en la que efectivamente se realizó la implantación del producto en el paciente.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'ImplantationDate'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Nombre del producto implantado. Campo obligatorio, alfanumérico y caracteres especiales, máximo 400 caracteres.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'ProductName'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Modelo del producto implantado. Campo obligatorio, alfanumérico y caracteres especiales, máximo 400 caracteres.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'Model'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Número de lote del producto implantado. Campo obligatorio, alfanumérico y caracteres especiales, máximo 400 caracteres.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'LotNumber'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Número de serie del producto implantado. Campo obligatorio, alfanumérico y caracteres especiales, máximo 400 caracteres.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'SerialNumber'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Fabricante del producto implantado. Campo obligatorio, alfanumérico y caracteres especiales, máximo 400 caracteres.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'Manufacturer'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Dirección del fabricante del producto implantado. Campo obligatorio, alfanumérico y caracteres especiales, máximo 400 caracteres.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'ManufacturerAddress'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Centro de atención donde se realizó el registro (referencia a ADCENATEN.CODCENATE).' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'CODCENATE'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Unidad funcional donde se realizó el registro (referencia a ADUNIFUN.UFUCODIGO).' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'UFUCODIGO'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Usuario/profesional que creó el registro. Columna de auditoría de creación.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'CODPROSALCREATE'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Fecha y hora en que se creó/registró el implante. Columna de auditoría de creación.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'CreationDate'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Estado del registro: 1 = Activo, 0 = Eliminado (lógicamente).' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Usuario que eliminó (lógicamente) el registro. Nulo mientras el registro esté activo. Columna de auditoría de eliminación.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'CODPROSALDEL'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Fecha y hora en que se eliminó (lógicamente) el registro. Nulo mientras el registro esté activo. Columna de auditoría de eliminación.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration', 
@level2type=N'COLUMN',@level2name=N'DeletionDate'
GO

EXEC sys.sp_addextendedproperty 
@name=N'MS_Description', 
@value=N'Registro de tarjetas de implante asociadas a un paciente. Se diligencia desde el formulario FrmRegistroTarjetaImplantes y se imprime mediante el reporte rptHCImplantCard.' , 
@level0type=N'SCHEMA',@level0name=N'dbo', 
@level1type=N'TABLE',@level1name=N'ImplantCardRegistration'
GO