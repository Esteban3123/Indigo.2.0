'***********************************************************************
' Assembly         : Infrastructura.Data.ModelRepository.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 04-08-2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class AntineoplasicoMedicationRepository
    Inherits GenericRepository(Of AntineoplasicoMedication)
    Implements IAntineoplasicoMedicationRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context">el contexto.</param>
    ''' 
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="statesAPM"></param>
    ''' <returns></returns>
    Public Function GetAntineoplasicoMedicationByStates(statesAPM As String) As List(Of Domain.Entities.AntineoplasicoMedication) Implements IAntineoplasicoMedicationRepository.GetAntineoplasicoMedicationByStates
        If String.IsNullOrEmpty(statesAPM) Then
            Throw New ArgumentNullException("states")
        End If
        Dim listaAPM = (From d As AntineoplasicoMedication In _context.AntineoplasicoMedication.Include("ATC") Where statesAPM.Contains(CStr(d.StateAPM)) Select d)
        If listaAPM IsNot Nothing Then
            For Each res In listaAPM
                res.CodeNameATC = String.Concat(res.ATC.Code, " - ", res.ATC.Name)
                Select Case res.ClassificationId
                    Case 1 : res.NameClassification = "Bleomicina"
                    Case 2 : res.NameClassification = "Busulfano"
                    Case 3 : res.NameClassification = "Capecitabina"
                    Case 4 : res.NameClassification = "Carboplatino"
                    Case 5 : res.NameClassification = "Ciclofosfamida"
                    Case 6 : res.NameClassification = "Ciclosporina"
                    Case 7 : res.NameClassification = "Cisplatino"
                    Case 8 : res.NameClassification = "Citarabina"
                    Case 9 : res.NameClassification = "Clorambucilo"
                    Case 10 : res.NameClassification = "Dacarbazina"
                    Case 11 : res.NameClassification = "Doxorubicina"
                    Case 12 : res.NameClassification = "Etopósido"
                    Case 13 : res.NameClassification = "Fluorouracilo"
                    Case 14 : res.NameClassification = "Gemcitabina"
                    Case 15 : res.NameClassification = "Imatinib"
                    Case 16 : res.NameClassification = "Interferón Alfa"
                    Case 17 : res.NameClassification = "Melfalan"
                    Case 18 : res.NameClassification = "Mercaptopurina"
                    Case 19 : res.NameClassification = "Metotrexato"
                    Case 20 : res.NameClassification = "Paclitaxel"
                    Case 21 : res.NameClassification = "Pegfilgrastim"
                    Case 22 : res.NameClassification = "Procarbazina"
                    Case 23 : res.NameClassification = "Rituximab"
                    Case 24 : res.NameClassification = "Tamoxifeno"
                    Case 25 : res.NameClassification = "Tioguanina"
                    Case 26 : res.NameClassification = "Trastuzumab"
                    Case 27 : res.NameClassification = "Vinblastina"
                    Case 28 : res.NameClassification = "Vincristina"
                    Case 29 : res.NameClassification = "Prednisona"
                    Case 30 : res.NameClassification = "Prednisolona"
                    Case 31 : res.NameClassification = "Metilprednisolona"
                    Case 32 : res.NameClassification = "Dexametasona"
                    Case 33 : res.NameClassification = "Otra clasificación de antineoplásico"
                    Case Else
                End Select
            Next
            Return listaAPM.ToList
        Else
            Return New List(Of AntineoplasicoMedication)
        End If
    End Function


    ''' <summary>
    ''' Guarda medicamentos con resistencia bacteriana
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveAntineoplasicoMedication(Xml As String, UserCode As String) As SP_SaveAntineoplasicoMedication_Result Implements IAntineoplasicoMedicationRepository.SP_SaveAntineoplasicoMedication
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveAntineoplasicoMedication(Xml, UserCode).SingleOrDefault
    End Function
End Class
