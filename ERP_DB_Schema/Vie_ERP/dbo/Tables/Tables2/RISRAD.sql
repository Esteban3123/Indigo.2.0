CREATE TABLE [dbo].[RISRAD] (
    [FLUTIEMP]      INT            NULL,
    [NUMEXPO]       INT            NULL,
    [DISTAFUEDECT]  VARCHAR (16)   NULL,
    [DISTAFUEPAC]   VARCHAR (16)   NULL,
    [DOSISENTRA]    INT            NULL,
    [DOSISENTRAMGY] VARCHAR (16)   NULL,
    [AREAEXP]       INT            NULL,
    [AREDOSPROD]    VARCHAR (16)   NULL,
    [COMDOSIS]      VARCHAR (1024) NULL,
    [RISORDENAUTO]  VARCHAR (20)   NOT NULL,
    [AUTO]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TAMPLACA]      VARCHAR (20)   NULL,
    [NUMPLACAS]     INT            NULL,
    [NUMDISPARO]    INT            NULL,
    [NUMPROCED]     INT            NULL,
    [PLACASBUENAS]  INT            NULL,
    [PLACASMALAS]   INT            NULL,
    [SOBREEXPO]     BIT            NULL,
    [MOTSOBREEXPO]  VARCHAR (200)  NULL,
    [PARTECUERPO]   VARCHAR (20)   NULL,
    [OBSERVAC]      VARCHAR (200)  NULL,
    CONSTRAINT [PK_RISRAD] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'observaciones ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'OBSERVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'parte del cuerpo en la que se tomo el examen ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PARTECUERPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo del motivo de sobreexposicion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'MOTSOBREEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'sobreexposicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'SOBREEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de placas malas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PLACASMALAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de placas buenas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'PLACASBUENAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMPROCED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de disparos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMDISPARO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de placas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMPLACAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tamaño de las placas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'TAMPLACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comments on Radiation Dose:  User-defined comments on any special conditions related to radiation dose encountered during this Performed Procedure Step.  DICOM TAG (0040,0310)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'COMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Image and Fluoroscopy Area Dose Product:  Total area-dose-product to which the patient was exposed, accumulated over the complete Performed Procedure Step and measured in dGy*cm*cm, including fluoroscopy.  DICOM TAG (0018,115E)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AREDOSPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exposed Area:  Typical dimension of the exposed area at the detector plane. If Rectangular: row dimension followed by column; if Round: diameter. Measured in mm.  DICOM TAG (0040,0303)  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'AREAEXP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entrance Dose in mGy:  Average entrance dose value measured in mGy at the surface of the patient­ during this Performed Procedure Step.  DICOM TAG (0040,8302)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DOSISENTRAMGY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entrance Dose:   Average entrance dose value measured in dGy at the surface of the patient­ during this Performed Procedure Step.  DICOM TAG (0040,0302)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DOSISENTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distance Source to Entrance:   Distance in mm from the source to the surface of the patient closest to the source during this Performed Procedure Step.  DICOM TAG (0040,0306)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DISTAFUEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distance Source to Detector:  Distance in mm from the source to detector center.  DICOM TAG (0018,1110)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'DISTAFUEDECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Number of Exposures:  Total number of exposures made during this Performed Procedure Step. The number includes non-digital and digital expo­sures.   DICOM TAG (0040,0301)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'NUMEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total Time of Fluoroscopy:  Total duration of X-Ray exposure during fluoroscopy in seconds (pedal time) during this Performed Procedure Step.  DICOM TAG (0040,0300)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISRAD', @level2type = N'COLUMN', @level2name = N'FLUTIEMP';

