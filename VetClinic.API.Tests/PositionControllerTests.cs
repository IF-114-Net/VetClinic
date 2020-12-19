﻿using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.Controllers;
using VetClinic.API.DTO.PositionDTO;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using Xunit;

namespace VetClinic.API.Tests
{
    public class PositionControllerTests
    {
        private readonly Mock<IPositionService> positionServiceMock;
        private readonly Mock<IMapper> mapper;
        private readonly PositionController positionController;
        public PositionControllerTests()
        {
            var fixture = new Fixture();
            positionServiceMock = fixture.Freeze<Mock<IPositionService>>();
            mapper = fixture.Freeze<Mock<IMapper>>();
            positionController = new PositionController(positionServiceMock.Object, mapper.Object);

        }


        [Theory, AutoMoqData]
        public async Task GetAll_Positions_ReturnsTrue(
            [Frozen] List<Position> positions,
            [Frozen] List<PositionDTO> positionsDTO)
        {
            // Arrange
            positionServiceMock.Setup(m => m.GetPositionAsync())            
                .ReturnsAsync(positions);
            mapper.Setup(m => m.Map<ICollection<PositionDTO>>(positions))
                .Returns(positionsDTO);            

            // Act
            var actualResult = await positionController.Index();

            // Assert      
            var result = actualResult.Result as OkObjectResult; 
            
            Assert.Equal(positionsDTO, result.Value );
            Assert.True(actualResult.Result is OkObjectResult);
            positionServiceMock.Verify(m => m.GetPositionAsync(), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task GetById_PositionId_ReturnsPositionWithRequestedId(
           [Frozen] Position position,
           [Frozen] PositionDTO positionDTO )
        {
            // Arrange           
            positionServiceMock.Setup(p => p.GetPositionAsync(position.Id))
                .ReturnsAsync(position);
            mapper.Setup(m => m.Map<PositionDTO>(position))
                .Returns(positionDTO);          

            // Act
            var actualResult = await positionController.Show(position.Id);

            // Assert    
            var result = actualResult.Result as OkObjectResult;
            Assert.Equal(positionDTO, result.Value);
            Assert.True(actualResult.Result is OkObjectResult);
            positionServiceMock.Verify(m => m.GetPositionAsync(position.Id), Times.Once);
        }


        [Fact]
        public async Task GetById_PositionId_ReturnsNotFound__withNewMock()
        {
            // Arrange     
            var positionServiceMock1 =new Mock<IPositionService>();
            var mapper1 = new Mock<IMapper>();
            Position position = new Position { Id = 1 };
            PositionDTO positionDTO = new PositionDTO { Id = 1 }; 
            
            positionServiceMock1.Setup(p => p.GetPositionAsync(position.Id))
                .ReturnsAsync(null as Position);
            mapper1.Setup(m => m.Map<PositionDTO>(position))
                .Returns(positionDTO);

            var positionController1 = new PositionController(positionServiceMock1.Object, mapper1.Object);

            // Act
            var actualResult = await positionController1.Show(position.Id);

            // Assert                          
            Assert.True(actualResult.Result is NotFoundResult);
            positionServiceMock1.Verify(m => m.GetPositionAsync(position.Id), Times.Once);

        }
    

        [Theory, AutoMoqData]
        public async Task Post_PositionDTO_ReturnsPositionDTO(
           [Frozen] Position position,
           [Frozen] PositionDTO positionDTO)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.AddPosition(position))
                .ReturnsAsync(position);            
            mapper.Setup(m => m.Map<Position>(positionDTO))
                .Returns(position);            

            // Act
            var actualResult = await positionController.Create(positionDTO);

            // Assert 
            var result = actualResult.Result as OkObjectResult;
            Assert.Equal(positionDTO, result.Value);
            positionServiceMock.Verify(m => m.AddPosition(position), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Put_PositionDTO_ReturnsNoContent(
           [Frozen] Position position,
           [Frozen] PositionDTO positionDTO)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.UpdatePosition(position,position.Id))
                .ReturnsAsync(true);
            mapper.Setup(m => m.Map<Position>(positionDTO))
                .Returns(position);            

            // Act
            var actualResult = await positionController.Update(positionDTO,position.Id);

            // Assert             
            Assert.True(actualResult.Result is NoContentResult);
            positionServiceMock.Verify(m => m.UpdatePosition(position, position.Id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Put_PositionDTO_ReturnsNotFound(
           [Frozen] Position position,
           [Frozen] PositionDTO positionDTO)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.UpdatePosition(position, position.Id))
                .ReturnsAsync(false);
            mapper.Setup(m => m.Map<Position>(positionDTO))
                .Returns(position);            

            // Act
            var actualResult = await positionController.Update(positionDTO, position.Id);

            // Assert             
            Assert.True(actualResult.Result is NotFoundResult);
            Assert.NotNull(actualResult);
        }

        [Theory, AutoMoqData]
        public async Task Delete_PositionId_ReturnsNoContent(
           [Frozen] int id)
        {
            // Arrange            
            positionServiceMock.Setup(m => m.RemovePosition(id))
                .ReturnsAsync(true); 

            // Act
            var actualResult = await positionController.Destroy(id);

            // Assert             
            Assert.True(actualResult is NoContentResult);
            positionServiceMock.Verify(m => m.RemovePosition(id), Times.Once);
        }


        [Theory, AutoMoqData]
        public async Task Delete_PositionId_ReturnsFound(
           [Frozen] int id )
        {
            // Arrange            
            positionServiceMock.Setup(m => m.RemovePosition(id))
                .ReturnsAsync(false);            

            // Act
            var actualResult = await positionController.Destroy(id);

            // Assert             
            Assert.True(actualResult is NotFoundResult);
            positionServiceMock.Verify(m => m.RemovePosition(id), Times.Once);
        }
    }
}
