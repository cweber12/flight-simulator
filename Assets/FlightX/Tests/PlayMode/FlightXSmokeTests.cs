using System.Collections;
using FlightX.Aircraft;
using FlightX.Input;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FlightX.Tests
{
    public class FlightXSmokeTests
    {
        [UnityTest]
        public IEnumerator CoreAircraftComponentsCanBeCreatedWithoutSceneWiring()
        {
            GameObject aircraft = new GameObject("Temporary Aircraft");

            try
            {
                Rigidbody rb = aircraft.AddComponent<Rigidbody>();
                AircraftInput input = aircraft.AddComponent<AircraftInput>();
                AircraftPhysics physics = aircraft.AddComponent<AircraftPhysics>();
                AircraftController controller = aircraft.AddComponent<AircraftController>();

                yield return null;

                Assert.That(rb, Is.Not.Null);
                Assert.That(input, Is.Not.Null);
                Assert.That(physics, Is.Not.Null);
                Assert.That(controller, Is.Not.Null);
            }
            finally
            {
                Object.Destroy(aircraft);
            }
        }
    }
}
