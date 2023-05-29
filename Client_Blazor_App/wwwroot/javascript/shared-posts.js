/*
// shared-posts.js

// Function to create HTML cards for the shared posts
function createPostCard(post) {
    // Extract post data
    const { sharedBy, sharedDate, comment, exercises } = post;

    // Create the card element
    const card = document.createElement('div');
    card.classList.add('post-card');

    // Add content to the card
    const username = document.createElement('h3');
    username.textContent = sharedBy; // Assuming sharedBy contains the username
    card.appendChild(username);

    const date = document.createElement('p');
    date.textContent = sharedDate; // Assuming sharedDate is in a suitable format
    card.appendChild(date);

    const commentText = document.createElement('p');
    commentText.textContent = comment;
    card.appendChild(commentText);

    const exercisesList = document.createElement('ul');
    exercises.forEach((exercise) => {
        const exerciseItem = document.createElement('li');
        exerciseItem.textContent = exercise.title; // Assuming exercise has a 'title' property
        exercisesList.appendChild(exerciseItem);
    });
    card.appendChild(exercisesList);

    return card;
}

// Function to display the shared posts in the HTML container
function displaySharedPosts(sharedPosts) {
    const sharedPostsContainer = document.getElementById('sharedPostsContainer');

    // Create cards for each shared post
    const postCards = sharedPosts.map((post) => createPostCard(post));

    // Add the cards to the container
    postCards.forEach((card) => {
        sharedPostsContainer.appendChild(card);
    });
}
*/
