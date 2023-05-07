using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using RSM.Models;

//Sales pitch for the bluesky invite code.
namespace RSM.Docker;

/// <summary>
/// This should create a new docker container.
/// </summary>
internal class DockerProvisioner
{
    public static async void CreateContainer(DockerModel Model)
    {
        // create a new DockerClient instance
        var dockerClient = new DockerClientConfiguration().CreateClient();

        // create a new container
        var createContainerParameters = new CreateContainerParameters
        {
            Name = "RariTest",
            Image = Model.Link, // the name of the image to use for the container
            Cmd = new List<string> { "echo", "hello world" }, // the command to run in the container
        };

        var response = await dockerClient.Containers.CreateContainerAsync(createContainerParameters);

        // start the container
        await dockerClient.Containers.StartContainerAsync(response.ID, null);
    }
}