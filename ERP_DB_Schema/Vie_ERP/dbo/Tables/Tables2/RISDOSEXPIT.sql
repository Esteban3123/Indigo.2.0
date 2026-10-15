CREATE TABLE [dbo].[RISDOSEXPIT] (
    [MODRAD]     VARCHAR (16)   NULL,
    [KVP]        VARCHAR (16)   NULL,
    [XRAYTUCUA]  VARCHAR (16)   NULL,
    [TIEMPEXP]   VARCHAR (12)   NULL,
    [TIPOFILT]   VARCHAR (16)   NULL,
    [MATFILT]    VARCHAR (16)   NULL,
    [COMDOSIS]   VARCHAR (1042) NULL,
    [AUTO]       INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RISRADAUTO] INT            NOT NULL,
    CONSTRAINT [PK_RISDOSEXPIT] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'AUTO de la tabla RISRAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'RISRADAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comments on Radiation Dose:  User-defined comments on any special conditions related to radiation dose encountered during during the episode described by this Exposure Dose Sequence Item.  DICOM TAG (0040,0310)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'COMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Filter Material:  The X-Ray absorbing material used in the filter. May be multi-valued. See Section C.8.7.10 and Section C.8.15.3.9 (for enhanced CT) for Defined Terms.  DICOM TAG (0018,7050)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'MATFILT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Filter Type:  Type of filter(s) inserted into the X-Ray beam (e.g., wedges). See Section C.8.7.10 and Section C.8.15.3.9 (for enhanced CT) for Defined Terms.  DICOM TAG (0018,1160)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'TIPOFILT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exposure Time:  Time of x-ray exposure or fluoroscopy in msec.  DICOM TAG (0018,1150)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'TIEMPEXP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'X-Ray Tube Current in µA:  X-Ray Tube Current in µA. An average in the case of fluoroscopy (continuous radiation mode).  DICOM TAG (0018,8151)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'XRAYTUCUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'KVP:   Peak kilo voltage output of the x-ray generator used. An average in the case of fluoroscopy (continuous radiation mode).  DICOM TAG (0018,0060)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'KVP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Radiation Mode:   Specifies X-Ray radiation mode.  Enumerated Values:    - CONTINUOUS  - PULSED  DICOM TAG (0018,115A)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RISDOSEXPIT', @level2type = N'COLUMN', @level2name = N'MODRAD';

