using Xunit;
using Moq;
using task04;

namespace task04.Tests
{
    public class SpaceshipTests
    {
        [Fact]
        public void Cruiser_ShouldHaveCorrectStats()
        {
            ISpaceship cruiser = new Cruiser();
            Assert.Equal(50, cruiser.Speed);
            Assert.Equal(100, cruiser.FirePower);
        }

        [Fact]
        public void Fighter_ShouldBeFasterThanCruiser()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();
            Assert.True(fighter.Speed > cruiser.Speed);
        }

        [Fact]
        public void Fighter_ShouldHaveWeakerFirepower()
        {
            var fighter = new Fighter();
            var cruiser = new Cruiser();
            Assert.True(fighter.FirePower < cruiser.FirePower);
        }

        [Fact]
        public void MoveForward_ShouldIncreaseDistance()
        {
            var ship = new Cruiser();
            var initialSpeed = ship.Speed;
            ship.MoveForward();
            ship.MoveForward();
        }

        [Fact]
        public void Rotate_ShouldHandlePositiveAngles()
        {
            var ship = new Fighter();
            ship.Rotate(90);
            ship.Rotate(45);
        }

        [Fact]
        public void Rotate_ShouldHandleNegativeAngles()
        {
            var ship = new Cruiser();
            ship.Rotate(-90);
            ship.Rotate(-45);
        }

        [Fact]
        public void Fire_ShouldRegisterShots()
        {
            var ship = new Fighter();
            ship.Fire();
            ship.Fire();
        }

        [Fact]
        public void Rotation_ShouldWrapAround360()
        {
            var ship = new Cruiser();
            ship.Rotate(400);
            ship.Rotate(-500);
        }
    }
}
