﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using OSPSuite.BDDHelper.Extensions;

namespace OSPSuite.BDDHelper
{
   public abstract class concern_for_BDDExtensions : StaticContextSpecification
   {
      protected override void Context()
      {
      }
   }

   public class When_testing_numerical_relations : concern_for_BDDExtensions
   {
      [Test]
      public void should_return_the_expected_values()
      {
         3.ShouldBeGreaterThan(2);
         1.ShouldBeEqualTo(1);
         1.ShouldBeSmallerThanOrEqualTo(1);
         1.ShouldBeSmallerThan(3);
         1.ShouldBeSmallerThanOrEqualTo(3);
         3.ShouldBeGreaterThanOrEqualTo(1);
         3.ShouldBeGreaterThanOrEqualTo(3);
         0.553.ShouldBeEqualTo(0.55, 1e-2);
         0.553f.ShouldBeEqualTo(0.55f, 1e-2);
      }
   }

   public class When_testing_boolean_relations : concern_for_BDDExtensions
   {
      [Observation]
      public void should_return_the_execpted_values()
      {
         true.ShouldBeTrue();
         false.ShouldBeFalse();
      }
   }

   public class When_testing_instances_relations : concern_for_BDDExtensions
   {
      private Parameter _parmaeter;

      protected override void Context()
      {
         base.Context();
         _parmaeter = new Parameter();
      }

      [Observation]
      public void should_return_the_execpted_values()
      {
         _parmaeter.ShouldBeAnInstanceOf<Parameter>();
         _parmaeter.ShouldBeAnInstanceOf(typeof(Parameter));
      }
   }

   public class When_testing_enumeration_relations : concern_for_BDDExtensions
   {
      private string _value1;
      private string _value2;
      private string _value3;
      private string[] _values1And2;

      protected override void Context()
      {
         base.Context();
         _value1 = "AA";
         _value2 = "BB";
         _value3 = "CC";
         _values1And2 = new[] {_value1, _value2,};
      }

      [Observation]
      public void should_return_the_execpted_values()
      {
         new[] {_value1, _value2}.ShouldOnlyContain(_value1, _value2);
         new[] {_value1, _value2}.ShouldOnlyContain(_values1And2);
         _values1And2.ShouldOnlyContain(_value1, _value2);
         _values1And2.ShouldOnlyContain(_values1And2);
         new[] {_value1, _value2}.ShouldOnlyContainInOrder(_value1, _value2);
         new[] {_value1, _value2, _value3}.ShouldContain(_value1, _value3);
         new[] {_value1, _value3}.ShouldNotContain(_value2);
         new List<string>().ShouldBeEmpty();
         _values1And2.ShouldNotBeEmpty();
      }
   }

   public class When_testing_equality_relations : concern_for_BDDExtensions
   {
      private Enumerable<Parameter> _parameterEnumerable1;
      private Enumerable<Parameter> _parameterEnumerable2;
      private Parameter _parameter;
      private List<Parameter> _parameterList1;
      private List<Parameter> _parameterList2;

      protected override void Context()
      {
         base.Context();
         _parameter = new Parameter();
         _parameterEnumerable1 = new Enumerable<Parameter> {_parameter};
         _parameterEnumerable2 = new Enumerable<Parameter> {_parameter};
         _parameterList1 = new List<Parameter> {_parameter};
         _parameterList2 = new List<Parameter> {_parameter};
      }

      [Observation]
      public void should_return_the_expected_values_when_comparing_primitive_enumerable()
      {
         _parameterList1.ShouldBeEqualTo(_parameterList2);
      }

      [Observation]
      public void should_return_the_expected_values_when_comparing_custom_enumerable()
      {
         _parameterEnumerable1.ShouldNotBeEqualTo(_parameterEnumerable2);
         _parameterEnumerable1.ShouldOnlyContainInOrder(_parameterEnumerable2);
      }
   }

   public class When_testing_exception_being_thrown : concern_for_BDDExtensions
   {
      private readonly Parameter _parameter = null;

      [Observation]
      public void should_return_the_expected_values()
      {
         The.Action(() => _parameter.Name = "Toto").ShouldThrowAn<NullReferenceException>();
      }
   }
}