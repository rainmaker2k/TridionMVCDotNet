﻿using System;
using Tridion.Extensions.DynamicDelivery.ContentModel;
using Tridion.Extensions.DynamicDelivery.ContentModel.Exceptions;
using Dynamic = Tridion.Extensions.DynamicDelivery.ContentModel;
using TCM = Tridion.ContentManager.ContentManagement;

namespace Tridion.Extensions.DynamicDelivery.Templates.Builder
{
   public class FieldsBuilder
   {
       public static Dynamic.Fields BuildFields(TCM.Fields.ItemFields tcmItemFields, int linkLevels, bool resolveWidthAndHeight, BuildManager manager)
      {
         var fields = new Fields();
         AddFields(fields, tcmItemFields, linkLevels, resolveWidthAndHeight, Dynamic.MergeAction.Replace, manager);
         return fields;
      }
       public static void AddFields(Dynamic.SerializableDictionary<string, Field> fields, TCM.Fields.ItemFields tcmItemFields, int linkLevels, bool resolveWidthAndHeight, Dynamic.MergeAction mergeAction, BuildManager manager)
      {
         foreach (TCM.Fields.ItemField tcmItemField in tcmItemFields)
         {
            try
            {
               if (fields.ContainsKey(tcmItemField.Name))
               {
                  if (mergeAction.Equals(Dynamic.MergeAction.Skip))
                  {
                     continue;
                  }
                  Dynamic.Field f = manager.BuildField(tcmItemField, linkLevels, resolveWidthAndHeight);
                  if (mergeAction.Equals(Dynamic.MergeAction.Replace))
                  {
                     fields.Remove(f.Name);
                     fields.Add(f.Name, f);
                  }
                  else
                  {
                     Field existingField = fields[f.Name];
                     switch (existingField.FieldType)
                     {
                      
                        case FieldType.ComponentLink:
                        case FieldType.MultiMediaLink:
                           foreach (Component linkedComponent in f.LinkedComponentValues)
                           {
                              bool valueExists = false;
                              foreach (Component existingLinkedComponent in existingField.LinkedComponentValues)
                              {
                                 if (linkedComponent.Id.Equals(existingLinkedComponent.Id))
                                 {
                                    // this value already exists
                                    valueExists = true;
                                    break;
                                 }
                              }
                              if (!valueExists)
                              {
                                 existingField.LinkedComponentValues.Add(linkedComponent);
                              }
                           }
                           break;
                        case FieldType.Date:
                           foreach (DateTime dateTime in f.DateTimeValues)
                           {
                              bool valueExists = false;
                              foreach (DateTime existingDateTime in existingField.DateTimeValues)
                              {
                                 if (dateTime.Equals(existingDateTime))
                                 {
                                    // this value already exists
                                    valueExists = true;
                                    break;
                                 }
                              }
                              if (!valueExists)
                              {
                                 existingField.DateTimeValues.Add(dateTime);
                              }
                           }
                           break;
                        case FieldType.Number:
                           foreach (int nr in f.NumericValues)
                           {
                              bool valueExists = false;
                              foreach (int existingNr in existingField.NumericValues)
                              {
                                 if (nr == existingNr)
                                 {
                                    // this value already exists
                                    valueExists = true;
                                    break;
                                 }
                              }
                              if (!valueExists)
                              {
                                 existingField.NumericValues.Add(nr);
                              }
                           }
                           break;
                        default:
                           foreach (string val in f.Values)
                           {
                              bool valueExists = false;
                              foreach (string existingVal in existingField.Values)
                              {
                                 if (val.Equals(existingVal))
                                 {
                                    // this value already exists
                                    valueExists = true;
                                    break;
                                 }
                              }
                              if (!valueExists)
                              {
                                 existingField.Values.Add(val);
                              }
                           }
                           break;
                     }
                  }
               }
               else
               {
                   Dynamic.Field f = manager.BuildField(tcmItemField, linkLevels, resolveWidthAndHeight);
                  fields.Add(f.Name, f);
               }
            }
            catch (FieldHasNoValueException)
            {
               // fail silently, field is not added to the list
            }
            catch (FieldTypeNotDefinedException)
            {
               // fail silently, field is not added to the list
            }
         }
      }
   }
}
