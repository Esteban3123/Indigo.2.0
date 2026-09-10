'***********************************************************************
' Assembly         : DistributedService.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

#End Region

<ServiceContract()>
Public Interface IMedicalFeesService
    Inherits IMedicalFeesBlockRecordMedicalFees, IMedicalFeesSequense, IMedicalFeesMedicalFeesContract, IMedicalFeesMedicalFeesCausation, IMedicalFeesMedicalFeesSettings, IMedicalFeesMedicalFeesLiquidation,
        IMedicalFeesMedicalFeesNote, IMedicalFeesHealthProfessionalContract, IMedicalGlosaMedicalFeesConcepts, IGlosaMedicalFeesService, IMedicalFeesCausationRecognition

End Interface
