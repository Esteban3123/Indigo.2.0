'***********************************************************************
' Assembly         : Infrastructura.Data.ModelRepository.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 06-04-2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class BacterialResistanceMedicationRepository
    Inherits GenericRepository(Of BacterialResistanceMedication)
    Implements IBacterialResistanceMedicationRepository

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
    ''' <param name="statesBRM"></param>
    ''' <returns></returns>
    Public Function GetBacterialResistanceMedicationByStates(statesBRM As String) As List(Of Domain.Entities.BacterialResistanceMedication) Implements IBacterialResistanceMedicationRepository.GetBacterialResistanceMedicationByStates
        If String.IsNullOrEmpty(statesBRM) Then
            Throw New ArgumentNullException("states")
        End If
        Dim listaBRM = (From d As BacterialResistanceMedication In _context.BacterialResistanceMedication.Include("ATC") Where statesBRM.Contains(CStr(d.StateBRM)) Select d)
        If listaBRM IsNot Nothing Then
            For Each res In listaBRM
                res.CodeNameATC = String.Concat(res.ATC.Code, " - ", res.ATC.Name)
            Next
            Return listaBRM.ToList
        Else
            Return New List(Of BacterialResistanceMedication)
        End If
    End Function


    ''' <summary>
    ''' Guarda medicamentos con resistencia bacteriana
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveBacterialResistanceMedication(Xml As String, UserCode As String) As SP_SaveBacterialResistanceMedication_Result Implements IBacterialResistanceMedicationRepository.SP_SaveBacterialResistanceMedication
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveBacterialResistanceMedication(Xml, UserCode).SingleOrDefault
    End Function
End Class
